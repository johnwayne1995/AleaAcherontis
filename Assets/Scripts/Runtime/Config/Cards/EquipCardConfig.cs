using Sirenix.OdinInspector;
using UnityEngine;

namespace Config
{
    public class EquipCardConfig : ScriptableObject
    {
        [LabelText("卡名")]
        public string name;

        [LabelText("描述")]
        public string dialog;

        [LabelText("卡面路径")]
        public string cardIconPath;
        
        [LabelText("对倍率的影响形式")]
        public DevilCardInfluenceType devilCardInfluenceType;

        [LabelText("参数值")]
        public int paramValue;
    }
    
    public enum DevilCardInfluenceType
    {
        Add,
        Multiplication 
    }
}
