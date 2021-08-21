using BepInEx;
using HarmonyLib;
using HarmonyLib.Tools;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace UltrakillSpeedometer {
    [BepInPlugin("com.twonukidevelopment.plugins.UltrakillSpeedometer", "UKSpeedometer", "1.0.0.0")]
    [BepInProcess("ULTRAKILL.exe")]
    public class Mod : BaseUnityPlugin
    {

        private static GameObject speedometer;
        private static Text speedometerText;
        
        private void Awake()
        {
            var harmony = new Harmony("com.twonukidevelopment.ukspeedometer");
            harmony.PatchAll();
            Debug.Log("ULTRAKILL SPEEDOMETER LOADED");
            HarmonyFileLog.Enabled = true;
        }
        
        [HarmonyPatch(typeof(HealthBar), "Update")]
        internal class HealthBarUpdatePatcher
        {
            [UsedImplicitly]
            [HarmonyPostfix]
            private static void AddSpeed()
            {
                speedometerText.text = $"{MonoSingleton<NewMovement>.Instance.rb.velocity.magnitude * 3.6f:000.00} km/h";
            }
        }
        
        [HarmonyPatch(typeof(HealthBar), "Start")]
        internal class HealthBarStartPatcher
        {
            [UsedImplicitly]
            [HarmonyPostfix]
            private static void PatchStart()
            {
                GameObject hpText = GameObject.Find("HP Text");
                speedometer = Instantiate(hpText, hpText.transform);
                speedometer.transform.localPosition = new Vector3(0.34f, -200, speedometer.transform.localPosition.z);
                
                speedometerText = speedometer.GetComponent<Text>();
                speedometerText.text = "";
                
                speedometer.SetActive(true);
            }
        }

        /*[HarmonyPatch]
        public class RailcannonStatusRPatch
        {
            [HarmonyReversePatch]
            [HarmonyPatch(typeof(RailcannonMeter), "RailcannonStatus")]
            public static bool RailcannonStatusR()
            {
                return false;
            }
        }*/

        [HarmonyPatch(typeof(RailcannonMeter), "CheckStatus")]
        internal class RailcannonMeterPatcher
        {
            [UsedImplicitly]
            [HarmonyPostfix]
            private static void PatchCheckStatus(RailcannonMeter __instance)
            {
                GameObject statsPanel = GameObject.Find("StatsPanel");
                    
                Vector3 sloc = speedometer.transform.localPosition;
                Vector3 sploc = statsPanel.transform.localPosition;
                    
                if (__instance.miniVersion.activeSelf)
                {
                    speedometer.transform.localPosition = new Vector3(0.34f, -55, sloc.z);
                    statsPanel.transform.localPosition = new Vector3(sploc.x, -200, sploc.z);
                }
                else
                {
                    speedometer.transform.localPosition = new Vector3(0.34f, -41, sloc.z);
                    statsPanel.transform.localPosition = new Vector3(sploc.x, -248, sploc.z);
                }
            }
        }
    }
}
