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
                GameObject hpText = GameObject.Find("FirstRoom/Player/Main Camera/HUD Camera/HUD/GunCanvas/StatsPanel/Filler/Panel (2)/Filler/HP Text");
                speedometer = Instantiate(hpText, hpText.transform);
                Vector3 loc = speedometer.transform.localPosition;

                speedometer.transform.localPosition = new Vector3(0.34f, -41, loc.z);

                speedometerText = speedometer.GetComponent<Text>();
                speedometerText.text = "";
                
                speedometer.SetActive(true);
            }
        }
    }
}
