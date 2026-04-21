using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Enums;
using Kingmaker.Blueprints.Classes.Spells;

namespace ImprovedPoP
{
    internal class VirulentPlagueFeat
    {
        private static readonly LogWrapper Logger = LogWrapper.Get(nameof(VirulentPlagueFeat));

        internal static void Create()
        {
            Logger.Info("Creating Virulent Plague feat");

            FeatureConfigurator.New("VirulentPlague", Guids.VirulentPlague)
                .SetDisplayName(Localization.VirulentPlagueName)
                .SetDescription(Localization.VirulentPlagueDescription)
                .SetGroups(FeatureGroup.Feat)
                .SetRanks(1)
                .AddIncreaseSpellDescriptorDC(
                    descriptor: SpellDescriptor.Disease,
                    bonusDC: 1,
                    modifierDescriptor: ModifierDescriptor.UntypedStackable,
                    spellsOnly: false)
                .Configure();

            FeatureConfigurator.New("GreaterVirulentPlague", Guids.GreaterVirulentPlague)
                .SetDisplayName(Localization.GreaterVirulentPlagueName)
                .SetDescription(Localization.GreaterVirulentPlagueDescription)
                .SetGroups(FeatureGroup.Feat)
                .SetRanks(1)
                .AddPrerequisiteFeature(Guids.VirulentPlague)
                .AddIncreaseSpellDescriptorDC(
                    descriptor: SpellDescriptor.Disease,
                    bonusDC: 1,
                    modifierDescriptor: ModifierDescriptor.UntypedStackable,
                    spellsOnly: false)
                .AddFeatureIfHasFact(
                    checkedFact: Guids.MythicVirulentPlague,
                    feature: Guids.MythicVirulentPlagueSubFeature,
                    not: false)
                .Configure();

            Logger.Info("Virulent Plague feat created successfully");
        }
    }
}