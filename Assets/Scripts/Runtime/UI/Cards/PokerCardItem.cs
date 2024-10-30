using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Managers;
using Modules;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class PokerCardItem : CardItemBase<PokerCard> , IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        /// <summary>  
        /// 卡牌扇形展开中心点  
        /// </summary>  
        public Vector3 root;  
        /// <summary>  
        /// 展开角度  
        /// </summary>  
        public float rot;  
        /// <summary>  
        /// 展开半径  
        /// </summary>  
        public float size;  
        /// <summary>  
        /// 动画速度  
        /// </summary>  
        public float animSpeed = 10;  
        
        public bool isSelected = false;
        public bool isEnable = false;
        
        private Text _pointText;
        private Text _cardNameTipText;

        private Button _cardBtn;
        private Image _bgImg;
        private int _index;
        private RectTransform _rectTransform;
        private CanvasGroup _pointTipCanvasGroup;

        private Vector3 _oriPos;
        private Vector3 _lastPos;

        private Action _cardSendStateChangedAc;
        

        public override void OnAwake()
        {
            base.OnAwake();
            _cardNameTipText = transform.Find("bg/pointTip/cardNameTipText").GetComponent<Text>();
            _pointText = transform.Find("bg/pointTip/pointText").GetComponent<Text>();

            _bgImg = transform.Find("bg").GetComponent<Image>();
            _cardBtn = _bgImg.GetComponent<Button>();
            _bgImg.material = Instantiate(Resources.Load<Material>("Mats/outline"));
            _rectTransform = GetComponent<RectTransform>();
            _pointTipCanvasGroup = transform.Find("bg/pointTip").GetComponent<CanvasGroup>();
            _cardBtn.onClick.AddListener(OnCardClicked);
            _oriPos = new Vector3((float)Screen.width / 2, -200, 0);
            _lastPos = Vector3.zero;
            _pointTipCanvasGroup.alpha = 0;
        }

        private void Update()
        {
            SetPos(); 
        }

        public void SetPos()
        {
            if(!isEnable || transform == null)
                return;
            
            //选中卡牌半径增加  
            float radius = isSelected ? size + 50 : size;
            //选中卡牌旋转归零  
            var position = transform.position;
            float rotZ = GetAngleInDegrees(root, position);
            //设置卡牌位置  
            float x = root.x + Mathf.Cos(rot) * radius;
            float y = root.y + Mathf.Sin(rot) * radius;
            _lastPos.x = x;
            _lastPos.y = y;
            // if (isSelected)
            // {
            //     _lastPos.y += 30;
            // }
            // else
            // {
            //     _lastPos.y = y;
            // }
            
            position = Vector3.Lerp(position, _lastPos, Time.deltaTime * animSpeed);
            transform.position = position;
            Quaternion rotationQuaternion = Quaternion.Euler(new Vector3(0, 0, rotZ));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationQuaternion, Time.deltaTime * animSpeed * 30);
        }

        /// <summary>  
        /// 获取两个向量之间的弧度值0-2π  
        /// </summary>    /// <param name="positionA">点A坐标</param>  
        /// <param name="positionB">点B坐标</param>  
        /// <returns></returns>    
        public static float GetAngleInDegrees(Vector3 positionA, Vector3 positionB)  
        {        
            // 计算从A指向B的向量  
            Vector3 direction = positionB - positionA;  
            // 将向量标准化  
            Vector3 normalizedDirection = direction.normalized;  
            // 计算夹角的弧度值  
            float dotProduct = Vector3.Dot(normalizedDirection, Vector3.up);  
            float angleInRadians = Mathf.Acos(dotProduct);  
  
            //判断夹角的方向：通过计算一个参考向量与两个物体之间的叉乘，可以确定夹角是顺时针还是逆时针方向。这将帮助我们将夹角的范围扩展到0到360度。  
            Vector3 cross = Vector3.Cross(normalizedDirection, Vector3.up);  
            if (cross.z > 0)  
            {            
                angleInRadians = 2 * Mathf.PI - angleInRadians;  
            }  
            // 将弧度值转换为角度值  
            float angleInDegrees = angleInRadians * Mathf.Rad2Deg;  
            return angleInDegrees;  
        }

        public void RefreshData(Vector3 root, float rot, float size)
        {
            this.root = root;
            this.rot = rot;
            this.size = size;
        }

        public override void InitCardItem(PokerCard config, Action cardStateChangeAc)
        {
            isEnable = true;
            _config = config;
            _cardNameTipText.text = config.name;
            _pointText.text = $"+{_config.basePoint}攻击";
            _cardSendStateChangedAc = cardStateChangeAc;
            transform.position = _oriPos;
            _bgImg.sprite = Resources.Load<Sprite>(config.bgPath);
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
                // var targetPos = _oriPos + new Vector3(0, 30, 0);
                // _rectTransform.DOAnchorPos(targetPos, 0.2f);
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
                // _rectTransform.DOAnchorPos(_oriPos, 0.2f);
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
            transform.DOScale(1.1f, 0.15f);
            _pointTipCanvasGroup.DOFade(1, 0.15f);

            // _index = transform.GetSiblingIndex();
            // transform.SetAsLastSibling();
            // _bgImg.material.SetColor("_lineColor", Color.yellow);
            // _bgImg.material.SetFloat("_lineWidth",10);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(1, 0.15f);
            _pointTipCanvasGroup.DOFade(0, 0.15f);

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
        
        public TweenerCore<Vector2, Vector2, VectorOptions> DoInitMoveAni(Vector2 endPos, float time = 0.5f)
        {
            this._oriPos = endPos;
            return _rectTransform.DOAnchorPos(endPos, time);
        }

        public void SetRectAnchorPos(Vector2 pos)
        {
            _rectTransform.anchoredPosition = pos;
        }
        
        public void DoScaleAni(float scale, float time)
        {
            _rectTransform.DOScale(scale, time);
        }
        
        public PokerCard GetCardConfig()
        {
            return _config;
        }
        
        public void ResetView()
        {
            _pointText.text = String.Empty;
            _cardSendStateChangedAc = null;
            _oriPos = Vector3.zero;
            _cardSendStateChangedAc = null;
            isSelected = false;
            isEnable = false;
            _rectTransform.localScale = Vector3.one;
            _bgImg.material.SetColor("_lineColor", Color.black);
            _bgImg.material.SetFloat("_lineWidth",1);
            SetRectAnchorPos(PokerCardPool.HidePos);
            _pointTipCanvasGroup.alpha = 0;
        }
    }
}
