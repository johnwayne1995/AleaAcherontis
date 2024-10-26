using System;
using Managers;
using UnityEngine.UI;
namespace UI
{
    public abstract class CardItemBase<T> : UIBase where T : CardBase
    {
        protected Image _bg;
        protected Action _cardStateChangeAc;
        protected T _config;

        private void Awake()
        {
            OnAwake();
        }

        public virtual void OnAwake()
        {
            _bg = transform.Find("bg").GetComponent<Image>();
        }

        public virtual void InitCardItem(T config, Action cardStateChangeAc)
        {
            _config = config;
            _cardStateChangeAc = cardStateChangeAc;
        }
    }
}
