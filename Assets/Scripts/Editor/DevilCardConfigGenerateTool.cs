using Config;
using UnityEditor;
using UnityEngine;

public class DevilConfigGenerateTool
{
    private const string CSAVE_PATH = "Assets/Resources/Configs/CardConfig/";
    private static string[] AllCards;

    [MenuItem("Assets/配置/魔神牌配置", false, 0)]
    static void ShowProfilerWindow()
    {
        var newConfig = ScriptableObject.CreateInstance<EnemyConfig>();
        var fullPath = CSAVE_PATH + "DevilCardsConfig.asset";

        AssetDatabase.CreateAsset(newConfig, fullPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
