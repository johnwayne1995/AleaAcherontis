using Sirenix.OdinInspector;
using UnityEngine;
namespace Runtime.Config
{
    public class GameConfig : ScriptableObject
    {
        [LabelText("最大出牌次数")]
        public int maxSendCardCount;
        
        [LabelText("最大弃牌次数")]
        public int maxFoldCardCount;
    }
}
