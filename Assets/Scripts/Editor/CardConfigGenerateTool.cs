using Config;
using UnityEditor;
using UnityEngine;

public class CardConfigGenerateTool
{
    private const string CSAVE_PATH = "Assets/Resources/Configs/CardConfig/";
    private static string[] AllCards;
    
    [MenuItem("Assets/配置/卡牌配置", false, 0)]
    static void ShowProfilerWindow()
    {
        var newConfig = ScriptableObject.CreateInstance<NormalCardsConfig>();
        var fullPath = CSAVE_PATH + "NormalCardsConfig.asset";
        
        AllCards  = new string[]
        {
            // 黑桃
            "As", "Ks", "Qs", "Js", "Ts","9s","8s","7s","6s","5s","4s","3s","2s",
            // 红桃
            "Ah", "Kh", "Qh", "Jh", "Th","9h","8h","7h","6h","5h","4h","3h","2h",
            // 方块
            "Ad", "Kd", "Qd", "Jd", "Td","9d","8d","7d","6d","5d","4d","3d","2d",
            // 梅花
            "Ac", "Kc", "Qc", "Jc", "Tc","9c","8c","7c","6c","5c","4c","3c","2c"
        };

        for (int i = 0; i < AllCards.Length; i++)
        {
            var card = new NormalCard();
            card.cardId = AllCards[i];

            switch (card.cardId[0])
            {
                case '2':
                    card.cardPoint = 13;
                    break;
                case 'A':
                    card.cardPoint = 12;
                    break;
                case 'K':
                    card.cardPoint = 11;
                    break;
                case 'Q':
                    card.cardPoint = 10;
                    break;
                case 'J':
                    card.cardPoint = 9;
                    break;
                case 'T':
                    card.cardPoint = 8;
                    break;
                case '9':
                    card.cardPoint = 7;
                    break;
                case '8':
                    card.cardPoint = 6;
                    break;
                case '7':
                    card.cardPoint = 5;
                    break;
                case '6':
                    card.cardPoint = 4;
                    break;
                case '5':
                    card.cardPoint = 3;
                    break;
                case '4':
                    card.cardPoint = 2;
                    break;
                case '3':
                    card.cardPoint = 1;
                    break;
            }
            
            newConfig.NormalCards.Add(card);
        }
        
        AssetDatabase.CreateAsset(newConfig, fullPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
