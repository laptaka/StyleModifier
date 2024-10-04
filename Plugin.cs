using BepInEx;
using Configgy;
using HarmonyLib;
using UnityEngine;

namespace StyleModifier;

[BepInPlugin("Lapt.StyleModifier", "StyleModifier", "1.0.0")]
[BepInDependency("Hydraxous.ULTRAKILL.Configgy")]
public class Plugin : BaseUnityPlugin
{
    [Configgable]
    public static float modifier = 1f;

    private ConfigBuilder _config;

    private void Awake()
    {
        _config = new ConfigBuilder("Lapt.StyleModifier", "StyleModifier");
        _config.BuildAll();

        var harmony = new Harmony($"{MyPluginInfo.PLUGIN_GUID}");
        harmony.PatchAll(typeof(PatchPoints));
    }

}

public class PatchPoints
{
    [HarmonyPatch(typeof(StyleHUD), "AddPoints")]
    [HarmonyPrefix]
    private static bool AddPoints(ref int points)
    {
        points = Mathf.RoundToInt(points * Plugin.modifier);
        return true;
    }
}