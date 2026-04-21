using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Enums;

namespace ImprovedPoP
{
    internal class MythicVirulentPlagueFeat
    {
        private static readonly LogWrapper Logger = LogWrapper.Get(nameof(MythicVirulentPlagueFeat));

        internal static void Create()
        {
            Logger.Info("Creating Mythic Virulent Plague feat");

            // Hidden sub-feature granting the extra +1 when Greater Virulent Plague is present
            FeatureConfigurator.New("MythicVirulentPlagueBonus", Guids.MythicVirulentPlagueSubFeature)
                .SetHideInUI(true)
                .SetHideInCharacterSheetAndLevelUp(true)
                .AddIncreaseSpellDescriptorDC(
                    descriptor: SpellDescriptor.Disease,
                    bonusDC: 1,
                    modifierDescriptor: ModifierDescriptor.UntypedStackable,
                    spellsOnly: false)
                .Configure();

            // Main mythic feat
            FeatureConfigurator.New("MythicVirulentPlague", Guids.MythicVirulentPlague)
                .SetDisplayName(Localization.MythicVirulentPlagueName)
                .SetDescription(Localization.MythicVirulentPlagueDescription)
                .SetGroups(FeatureGroup.MythicFeat)
                .SetIsClassFeature(true)
                .AddPrerequisiteFeature(Guids.VirulentPlague)
                .AddIncreaseSpellDescriptorDC(
                    descriptor: SpellDescriptor.Disease,
                    bonusDC: 1,
                    modifierDescriptor: ModifierDescriptor.UntypedStackable,
                    spellsOnly: false)
                .AddFeatureIfHasFact(
                    checkedFact: Guids.GreaterVirulentPlague,
                    feature: Guids.MythicVirulentPlagueSubFeature,
                    not: false)
                .Configure();

            Logger.Info("Mythic Virulent Plague feat created");
        }
    }
}