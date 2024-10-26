using System;
using DG.Tweening;
using Managers;
using Modules;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class PokerCardItem : CardItemBase<PokerCard> //, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Text _nameText;
        private Button _cardBtn;
        private Image _bgImg;
        private int _index;
        private RectTransform _rectTransform;
        private Vector3 _oriPos;

        private Action _cardSendStateChangedAc;
        
        public bool isSelected = false;


        public override void OnAwake()
        {
            base.OnAwake();
            _nameText = transform.Find("bg/nameText").GetComponent<Text>();
            _bgImg = transform.Find("bg").GetComponent<Image>();
            _cardBtn = _bgImg.GetComponent<Button>();
            _bgImg.material = Instantiate(Resources.Load<Material>("Mats/outline"));
            _rectTransform = GetComponent<RectTransform>();
            _cardBtn.onClick.AddListener(OnCardClicked);
        }

        public override void InitCardItem(PokerCard config, Action cardStateChangeAc)
        {
            _config = config;
            _nameText.text = ConvertIdToName(_config.id, out var col);
            _nameText.color = col;
            _cardSendStateChangedAc = cardStateChangeAc;
        }

        private string ConvertIdToName(string id, out Color col)
        {
            string outName = string.Empty;
            var number = id[0];
            var color = id[1];
            Color textCol = Color.black;
            switch (color)
            {
                case 's' :
                    outName = "♠";
                    textCol = Color.black;
                    break;
                case 'h' :
                    outName = "♥";
                    textCol = Color.red;
                    break;
                case 'd' :
                    outName = "♦";
                    textCol = Color.red;
                    break;
                case 'c' :
                    outName = "♣";
                    textCol = Color.black;
                    break;
            }

            var faceStr = number.ToString();
            if (number == 'T')
            {
                faceStr = "10";
            }
            
            outName += " " + faceStr;
            col = textCol;
            return outName;
        }
        
        private void OnCardClicked()
        {
            var fightCardMgr = GameManagerContainer.Instance.GetManager<FightCardManager>();
            
            var targetState = !isSelected;
            if (targetState && fightCardMgr.SetCardToWaitSend(_config))
            {
                var targetPos = _oriPos + new Vector3(0, 30, 0);
                _rectTransform.DOAnchorPos(targetPos, 0.2f);
                _bgImg.material.SetColor("_lineColor", Color.yellow);
                _bgImg.material.SetFloat("_lineWidth",7);
                isSelected = true;

                if (_cardSendStateChangedAc != null)
                {
                    _cardSendStateChangedAc.Invoke();
                }
            }
            else if (!targetState)
            {
                fightCardMgr.SetCardToHand(_config);
                _rectTransform.DOAnchorPos(_oriPos, 0.2f);
                _bgImg.material.SetColor("_lineColor", Color.black);
                _bgImg.material.SetFloat("_lineWidth",1);
                isSelected = false;
                
                if (_cardSendStateChangedAc != null)
                {
                    _cardSendStateChangedAc.Invoke();
                }
            }
        }

        /// <summary>
        /// 鼠标进入
        /// </summary>
        /// <param name="eventData"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnPointerEnter(PointerEventData eventData)
        {
            // transform.DOScale(1.1f, 0.15f);
            // _index = transform.GetSiblingIndex();
            // transform.SetAsLastSibling();
            // _bgImg.material.SetColor("_lineColor", Color.yellow);
            // _bgImg.material.SetFloat("_lineWidth",10);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            // transform.DOScale(1, 0.15f);
            // transform.SetSiblingIndex(_index);
            // _bgImg.material.SetColor("_lineColor", Color.black);
            // _bgImg.material.SetFloat("_lineWidth", 1);
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
        }
        
        public void OnDrag(PointerEventData eventData)
        {
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
        }
        
        public void DoInitMoveAni(Vector2 endPos, float time = 0.5f)
        {
            this._oriPos = endPos;
            this.GetComponent<RectTransform>().DOAnchorPos(endPos, time);
        }

        public void DoScaleAni(float scale, float time)
        {
            transform.DOScale(scale, time);
        }
        
        public PokerCard GetCardConfig()
        {
            return _config;
        }
        
        public void OnRecycle()
        {
            _cardSendStateChangedAc = null;
            GameObject.DestroyImmediate(this.gameObject);
        }
    }
}
