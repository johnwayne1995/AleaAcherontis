using System;
using Config;
using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public class EquipCardItem : CardItemBase<EquipCard>
    {
        private Transform _lockPanel;
        private Transform _emptyPanel;

        private Image _icon;
        private Text _namText;
        private Text _dialogText;

        private Text _damageText;
        private CanvasGroup _damageTipCanvasGroup;

        private GameObject _fxObj;
        
        public bool isEmpty;
        public EquipCard equipConfig;

        public override void OnAwake()
        {
            base.OnAwake();
            _lockPanel = transform.Find("lockPanel");
            _emptyPanel = transform.Find("emptyPanel");
            _fxObj = transform.Find("UIFX_Glow").gameObject;
            _namText = transform.Find("nameText").GetComponent<Text>();
            _dialogText = transform.Find("dialogText").GetComponent<Text>();
            _damageText = transform.Find("damagePointTip/damagePointTipText").GetComponent<Text>();
            _damageTipCanvasGroup = transform.Find("damagePointTip").GetComponent<CanvasGroup>();

            _icon = transform.Find("icon").GetComponent<Image>();
            _damageTipCanvasGroup.alpha = 0;
            _fxObj.SetActive(false);
        }

        public void Init(bool isLock, EquipCard cardConfig)
        {
            _lockPanel.gameObject.SetActive(isLock);
            isEmpty = cardConfig == null;
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

            equipConfig = config;
            _icon.sprite = Resources.Load<Sprite>(config.iconPath);
            _namText.text = config.name;
            _dialogText.text = config.dialog;
        }
        
        public void ShowEffect(Action onShowDamageOver)
        {
            switch (equipConfig.devilCardInfluenceType)
            {
                case DevilCardInfluenceType.Add:
                    _damageText.text = "+" + this._config.paramValue;
                    break;
                case DevilCardInfluenceType.Multiplication:
                    _damageText.text = "x" + this._config.paramValue;
                    break;
            }
            
            _damageTipCanvasGroup.transform.localScale = Vector3.zero;
            _damageTipCanvasGroup.transform.DOScale(1, 0.3f);
            _damageTipCanvasGroup.DOFade(1, 0.2f);
            _fxObj.SetActive(true);
            
            var fadeTw = _damageTipCanvasGroup.DOFade(0, 0.2f);
            fadeTw.SetDelay(0.6f);
            var hideTw = _damageTipCanvasGroup.transform.DOScale(0, 0.3f);
            hideTw.SetDelay(0.6f);
            hideTw.onComplete += () =>
            {
                _fxObj.SetActive(false);
                onShowDamageOver?.Invoke();
            };
        }
        
        public void Recycle()
        {
            GameObject.DestroyImmediate(this.gameObject);
        }
    }
}
