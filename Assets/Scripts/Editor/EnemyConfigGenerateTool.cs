using Config;
using UnityEditor;
using UnityEngine;

public class EnemyConfigGenerateTool
{
    private const string CSAVE_PATH = "Assets/Resources/Configs/EnemyConfigs/";
    private static string[] AllCards;

    [MenuItem("Assets/配置/敌人配置", false, 0)]
    static void ShowProfilerWindow()
    {
        var newConfig = ScriptableObject.CreateInstance<EnemyConfig>();
        var fullPath = CSAVE_PATH + "EnemyCardsConfig.asset";
        fullPath = AssetDatabase.GenerateUniqueAssetPath(fullPath);

        AssetDatabase.CreateAsset(newConfig, fullPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
