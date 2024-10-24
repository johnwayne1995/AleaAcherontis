using System;
using Config;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace UI
{
    public class CardItem : UIBase, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Text _nameText;

        private Image _bgImg;
        private int _index;
        
        private void Awake()
        {
            _nameText = transform.Find("bg/nameText").GetComponent<Text>();
            _bgImg = transform.Find("bg").GetComponent<Image>();
            _bgImg.material = Instantiate(Resources.Load<Material>("Mats/outline"));
        }
        
        public void Init(NormalCard cardConfig)
        {
            _nameText.text = cardConfig.cardName;
        }

        /// <summary>
        /// 鼠标进入
        /// </summary>
        /// <param name="eventData"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(1.2f, 0.15f);
            _index = transform.GetSiblingIndex();
            transform.SetAsLastSibling();
            _bgImg.material.SetColor("_lineColor", Color.yellow);
            _bgImg.material.SetFloat("_lineWidth",10);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(1, 0.15f);
            transform.SetSiblingIndex(_index);
            _bgImg.material.SetColor("_lineColor", Color.black);
            _bgImg.material.SetFloat("_lineWidth", 1);
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            throw new NotImplementedException();
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            throw new NotImplementedException();
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            throw new NotImplementedException();
        }
    }
}
