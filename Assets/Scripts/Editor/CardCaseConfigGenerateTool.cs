using System;
using Config;
using Managers;
using UnityEditor;
using UnityEngine;

public class CardCaseConfigGenerateTool
{
    private const string CSAVE_PATH = "Assets/Resources/Configs/CardConfig/";
    private static string[] AllCards;
    
    [MenuItem("Assets/配置/牌型配置", false, 0)]
    static void ShowProfilerWindow()
    {
        var newConfig = ScriptableObject.CreateInstance<CardCaseConfig>();
        var fullPath = CSAVE_PATH + "CardsCaseConfig.asset";
        
        foreach (CaseEnum day in Enum.GetValues(typeof(CaseEnum)))
        {
            if(day == CaseEnum.None)
                continue;
            
            var card = new CardCase();
            card.caseEnum = day;
            newConfig.CardCases.Add(card);
        }
        newConfig.CardCases.Reverse();
        AssetDatabase.CreateAsset(newConfig, fullPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
