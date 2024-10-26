using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace UI
{
    public class PokerCardPool
    {
        private Queue<PokerCardItem> _pool = new Queue<PokerCardItem>(20);
        private const int MAX_COUNT = 20;
        private Vector2 _hidePos = new Vector2(100000, 1000000);
        
        public PokerCardPool(Transform parent)
        {
            for (int i = 0; i < MAX_COUNT; i++)
            {
                GameObject obj = GameObject.Instantiate(Resources.Load("UI/PokerCardItem"), parent) as GameObject;
                var item = obj.AddComponent<PokerCardItem>();
                item.SetRectAnchorPos(_hidePos);
                EnqueItem(item);
            }
        }

        public PokerCardItem Alloc()
        {
            PokerCardItem obj = null;
            if (_pool.Count > 0)
            {
                obj = _pool.Dequeue();
            }

            return obj;
        }

        public void Free(PokerCardItem obj)
        {
            if (obj.gameObject)
            {
                _pool.Enqueue(obj);
                obj.ResetView();
                obj.SetRectAnchorPos(_hidePos);
            }
        }

        private void EnqueItem(PokerCardItem item)
        {
            _pool.Enqueue(item);
            item.SetRectAnchorPos(_hidePos);
        }

        /// <summary>
        /// 释放所有缓存中的UIComponent，切换场景的时候用
        /// </summary>
        public void ReleaseAll()
        {
            while (_pool.Count > 0)
            {
                GameObject.Destroy(_pool.Dequeue().gameObject);
            }
        }
    }
}
