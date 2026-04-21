using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using System;
using UnityModManagerNet;

namespace ImprovedPoP
{
    public class Main
    {
        private static readonly LogWrapper Logger = LogWrapper.Get(nameof(Main));

        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            try
            {
                Logger.Info("Loading ImprovedPoP mod");
                var harmony = new Harmony(modEntry.Info.Id);
                harmony.PatchAll();
                Logger.Info("ImprovedPoP mod loaded successfully");
                return true;
            }
            catch (Exception e)
            {
                Logger.Error("Failed to load ImprovedPoP", e);
                return false;
            }
        }

        // This patch fires after the game has finished loading all blueprints
        [HarmonyPatch(typeof(BlueprintsCache), nameof(BlueprintsCache.Init))]
        static class BlueprintsCache_Init_Patch
        {
            static bool Initialized = false;

            [HarmonyPostfix]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                try
                {
                    ExtraMutationFeat.Create();
                    VirulentPlagueFeat.Create();
                    MythicVirulentPlagueFeat.Create();
                    ThousandDiseasesFix.Apply();
                }
                catch (Exception e)
                {
                    LogWrapper.Get(nameof(BlueprintsCache_Init_Patch))
                        .Error("Failed to create blueprints", e);
                }
            }
        }
    }
}