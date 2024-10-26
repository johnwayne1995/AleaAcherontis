using Config;
using UnityEditor;
using UnityEngine;

public class EquipCardConfigGenerateTool
{
    private const string CSAVE_PATH = "Assets/Resources/Configs/CardConfig/";
    private static string[] AllCards;

    [MenuItem("Assets/配置/装备牌配置", false, 0)]
    static void ShowProfilerWindow()
    {
        var newConfig = ScriptableObject.CreateInstance<EquipCardConfig>();
        var fullPath = CSAVE_PATH + "EquipCardsConfig.asset";

        
        AssetDatabase.CreateAsset(newConfig, fullPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
