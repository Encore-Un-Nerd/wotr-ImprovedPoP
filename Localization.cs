using Kingmaker.Localization;
using Kingmaker.Localization.Shared;

namespace ImprovedPoP
{
    internal static class Localization
    {
        internal static LocalizedString CreateString(string key, string enText, string frText)
        {
            var localizedString = new LocalizedString();
    
            // m_Key is a private field, set it via reflection
            var keyField = typeof(LocalizedString).GetField("m_Key", 
                System.Reflection.BindingFlags.NonPublic | 
                System.Reflection.BindingFlags.Instance);
            keyField.SetValue(localizedString, key);

            var currentLocale = LocalizationManager.CurrentLocale;
            var text = currentLocale == Locale.frFR ? frText : enText;
            LocalizationManager.CurrentPack.PutString(key, text);

            return localizedString;
        }

        internal static readonly LocalizedString ExtraMutationName = CreateString(
            "ExtraMutation.Name",
            "Extra Mutation",
            "Mutation supplémentaire"
        );

        internal static readonly LocalizedString ExtraMutationDescription = CreateString(
            "ExtraMutation.Description",
            "The character unlocked an additional mutation for their Plague of Abaddon.\n" +
            "They must meet all prerequisites for the chosen mutation." +
            "This feat can be taken three times.",

            "Le personnage subit une nouvelle mutation du Fléau d'Abaddon.\n" +
            "Il doit satisfaire tous les prérequis de la mutation choisie.\n" +
            "Ce don peut être pris trois fois."
        );

        internal static readonly LocalizedString VirulentPlagueName = CreateString(
            "VirulentPlague.Name",
            "Virulent Plague",
            "Peste virulente"
        );

        internal static readonly LocalizedString VirulentPlagueDescription = CreateString(
            "VirulentPlague.Description",
            "The character's mastery of diseases make them harder to resist.\n" +
            "Increases the DC of all disease effects by +1.",
            "Les maladies du personnage sont plus difficiles à résister.\n" +
            "Augmente le DD de tous les effets de maladie de +1."
        );

        internal static readonly LocalizedString GreaterVirulentPlagueName = CreateString(
            "GreaterVirulentPlague.Name",
            "Greater Virulent Plague",
            "Peste virulente supérieure"
        );

        internal static readonly LocalizedString GreaterVirulentPlagueDescription = CreateString(
            "GreaterVirulentPlague.Description",
            "The character's diseases have become almost impossible to resist.\n" +
            "Increase the DC of all disease effects by an additional +1.",
            "Les maladies du personnage sont devenues presque impossibles à résister.\n" +
            "Augmente le DD de tous les effets de maladie de +1 supplémentaire."
        );

        internal static readonly LocalizedString MythicVirulentPlagueName = CreateString(
            "MythicVirulentPlague.Name",
            "Mythic Virulent Plague",
            "Peste Virulente Mythique"
        );

        internal static readonly LocalizedString MythicVirulentPlagueDescription = CreateString(
            "MythicVirulentPlague.Description",
            "Your plagues reach mythic potency. You gain a +1 bonus to the DC of all disease effects.\n" +
            "If you have Greater Virulent Plague, you gain an additional +1 bonus to the DC of all disease effects.",
            "Vos maladies atteignent une puissance mythique. Vous gagnez un bonus de +1 aux DD de tous vos effets de maladie.\n" +
            "Si vous possédez Peste Virulente Supérieure, vous gagnez un bonus supplémentaire de +1 aux DD de tous vos effets de maladie."
        );
    }
}