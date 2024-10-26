using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public class EquipCardItem : CardItemBase<EquipCard>
    {
        private Transform _lockPanel;
        private Transform _emptyPanel;

        private Text _namText;
        private Text _dialogText;
        
        public override void OnAwake()
        {
            base.OnAwake();
            _lockPanel = transform.Find("lockPanel");
            _emptyPanel = transform.Find("emptyPanel");
            
            _namText = transform.Find("nameText").GetComponent<Text>();
            _dialogText = transform.Find("dialogText").GetComponent<Text>();
        }

        public void Init(bool isLock, EquipCard cardConfig)
        {
            _lockPanel.gameObject.SetActive(isLock);

            if (isLock)
            {
                _emptyPanel.gameObject.SetActive(false);
                return;
            }
            
            if (cardConfig == null)
            {
                _emptyPanel.gameObject.SetActive(true);
                return;
            }
            
            InitCardItem(cardConfig, null);
        }
        
        public override void InitCardItem(EquipCard config, Action cardStateChangeAc)
        {
            base.InitCardItem(config, cardStateChangeAc);
            _namText.text = config.name;
            _dialogText.text = config.dialog;
        }
    }
}
