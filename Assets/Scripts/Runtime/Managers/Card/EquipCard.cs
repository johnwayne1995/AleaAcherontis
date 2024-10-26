using System;
using Config;
namespace Managers
{
    [Serializable]
    public class EquipCard : CardBase
    {
        public string dialog;
        public DevilCardInfluenceType devilCardInfluenceType;
        public int paramValue;
    }
}
