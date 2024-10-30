using Sirenix.OdinInspector;
using UnityEngine;
namespace Runtime.Config
{
    public class GameConfig : ScriptableObject
    {
        [LabelText("玩家血量")]
        public int hpMax;
    }
}
