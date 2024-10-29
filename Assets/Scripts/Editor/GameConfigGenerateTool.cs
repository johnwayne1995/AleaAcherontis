using Runtime.Config;
using UnityEditor;
using UnityEngine;

public class GameConfigGenerateTool
{
    private const string CSAVE_PATH = "Assets/Resources/Configs/";

    [MenuItem("Assets/配置/游戏配置", false, 0)]
    static void ShowProfilerWindow()
    {
        var newConfig = ScriptableObject.CreateInstance<GameConfig>();
        var fullPath = CSAVE_PATH + "GameConfig.asset";
        AssetDatabase.CreateAsset(newConfig, fullPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
