using System.Reflection;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace ImprovedPoP
{
    internal class ThousandDiseasesFix
    {
        private static readonly LogWrapper Logger = LogWrapper.Get(nameof(ThousandDiseasesFix));

        private const string AreaEffectGuid  = "b605810fa41547afaeda4024efac79ad";
        private const string DiseaseBuffGuid = "97345eb635074d8f9ba07433cae0ed36";
        private const string ShamanClassGuid = "145f1d3d360a7ad48bd95d392c81b38e";

        private static void SetPrivate(object obj, string fieldName, object value)
        {
            var field = obj.GetType().GetField(fieldName,
                BindingFlags.NonPublic | BindingFlags.Instance);
            field?.SetValue(obj, value);
        }

        internal static void Apply()
        {
            Logger.Info("Applying Thousand Diseases fix");

            var area = BlueprintTool.Get<BlueprintAbilityAreaEffect>(AreaEffectGuid);

            // Build ContextRankConfig via reflection
            var rankConfig = new ContextRankConfig();
            SetPrivate(rankConfig, "m_Type",          AbilityRankType.Default);
            SetPrivate(rankConfig, "m_BaseValueType", ContextRankBaseValueType.ClassLevel);
            SetPrivate(rankConfig, "m_Progression",   ContextRankProgression.AsIs);
            SetPrivate(rankConfig, "m_UseMin",        true);
            SetPrivate(rankConfig, "m_Min",           1);
            SetPrivate(rankConfig, "m_Class", new BlueprintCharacterClassReference[]
            {
                BlueprintTool.GetRef<BlueprintCharacterClassReference>(ShamanClassGuid)
            });

            area.AddComponents(rankConfig);
            Logger.Info("Added ContextRankConfig to area effect");

            // After adding the ContextRankConfig, also add ContextCalculateAbilityParamsBasedOnClass
            var calcParams = new ContextCalculateAbilityParamsBasedOnClass();
            SetPrivate(calcParams, "m_CharacterClass", 
                BlueprintTool.GetRef<BlueprintCharacterClassReference>(ShamanClassGuid));
            calcParams.StatType = StatType.Wisdom;
            area.AddComponents(calcParams);
            Logger.Info("Added ContextCalculateAbilityParamsBasedOnClass to area effect");

            // Fix DiceType from Zero to One
            var runAction = area.GetComponent<AbilityAreaEffectRunAction>();
            if (runAction == null)
            {
                Logger.Error("Could not find AbilityAreaEffectRunAction");
                return;
            }

            int fixedCount = 0;
            FixDuration(runAction.UnitEnter.Actions, ref fixedCount);
            Logger.Info($"Fixed {fixedCount} disease buff duration(s)");
        }

        private static void FixDuration(GameAction[] actions, ref int count)
        {
            foreach (var action in actions)
            {
                if (action is Conditional conditional)
                {
                    FixDuration(conditional.IfTrue.Actions, ref count);
                    FixDuration(conditional.IfFalse.Actions, ref count);
                }
                else if (action is ContextActionSavingThrow savingThrow)
                {
                    foreach (var sub in savingThrow.Actions.Actions)
                    {
                        if (sub is ContextActionConditionalSaved saved)
                            FixDuration(saved.Failed.Actions, ref count);
                    }
                }
                else if (action is ContextActionApplyBuff applyBuff
                    && applyBuff.Buff?.AssetGuid.ToString() == DiseaseBuffGuid
                    && applyBuff.DurationValue.DiceType == DiceType.Zero)
                {
                    applyBuff.DurationValue.DiceType = DiceType.One;
                    count++;
                }
            }
        }
    }
}