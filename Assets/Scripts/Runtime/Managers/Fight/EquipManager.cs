﻿using System.Collections.Generic;
using Config;

namespace Managers
{
    public class EquipManager : TGameManager<EquipManager>
    {
        public int MaxEquipSlotCount
        {
            get;
            private set;
        }
        
        public int CanEquipSlotCount
        {
            get;
            private set;
        }

        private List<EquipCard> _equipCardList = new List<EquipCard>();
        
        protected override void OnAwake()
        {
            base.OnAwake();
            MaxEquipSlotCount = 4;
            CanEquipSlotCount = 3;
        }

        protected override void OnEnterGame()
        {
            base.OnEnterGame();
        }

        public EquipCard GetEquipCardConfigByPos(int pos)
        {
            if (pos >= _equipCardList.Count)
            {
                return null;
            }

            return _equipCardList[pos];
        }

        public void AddEquipReward(EquipCardConfig reward)
        {
            if (_equipCardList.Count == CanEquipSlotCount)
            {
                //todo 装备已满
                return;
            }
            
            var equip = new EquipCard();
            equip.name = reward.name;
            equip.dialog = reward.dialog;
            equip.iconPath = reward.cardIconPath;
            equip.devilCardInfluenceType = reward.devilCardInfluenceType;
            equip.paramValue = reward.paramValue;
            _equipCardList.Add(equip);
        }
    }
}
