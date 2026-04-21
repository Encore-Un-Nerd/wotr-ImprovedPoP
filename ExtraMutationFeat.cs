using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Selection;

namespace ImprovedPoP
{
    internal class ExtraMutationFeat
    {
        private static readonly LogWrapper Logger = LogWrapper.Get(nameof(ExtraMutationFeat));

        internal static void Create()
        {
            Logger.Info("Creating Extra Mutation feat");

            // Grab the PlagueHexSelection blueprint so we can copy its feature list
            var plagueHexSelection = BlueprintTool.Get<BlueprintFeatureSelection>(Guids.PlagueHexSelection);

            // Build references to all 22 mutations
            var allMutations = new Blueprint<BlueprintFeatureReference>[]
            {
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation01),
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation02),
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation03),
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation04),
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation05),
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation06),
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation07),
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation08),
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation09),
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation10),
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation11),
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation12),
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation13),
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation14),
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation15),
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation16),
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation17),
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation18),
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation19),
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation20),
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation21),
                BlueprintTool.GetRef<BlueprintFeatureReference>(Guids.Mutation22),
            };

            FeatureSelectionConfigurator.New("ExtraMutation", Guids.ExtraMutation)
                // Name and description shown in the feat selection UI
                .SetDisplayName(Localization.ExtraMutationName)
                .SetDescription(Localization.ExtraMutationDescription)
                // Mark it as a feat so it appears in the feat selection list
                .SetGroups(Kingmaker.Blueprints.Classes.FeatureGroup.ShamanHex)
                // Limit usage to 3 times only
                .SetRanks(3)
                // Require the character to already have PlagueHexSelection (i.e. be a PoP)
                .AddPrerequisiteFeature(Guids.PlagueHexSelection)
                // Populate with all available mutations
                .SetAllFeatures(allMutations)
                // Allow taking mutations already taken via Carrier of Disease?
                // Set to false to prevent duplicates
                .SetIgnorePrerequisites(false)
                .Configure();

            // Manually add to ShamanHexSelection since its Groups field is empty
            FeatureSelectionConfigurator.For(Guids.ShamanHexSelection)
                .AddToAllFeatures(Guids.ExtraMutation)
                .Configure();

            // Add to ExtraShamanHexSelection (appears when taking Extra Hex feat)
            FeatureSelectionConfigurator.For(Guids.ExtraShamanHexSelection)
                .AddToAllFeatures(Guids.ExtraMutation)
                .Configure();

            Logger.Info("Extra Mutation feat created successfully");
        }
    }
}