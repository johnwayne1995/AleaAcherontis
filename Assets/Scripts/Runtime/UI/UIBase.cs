using System;
using Managers;
using Modules;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIBase : MonoBehaviour
    {
        public virtual void OnShow()
        {
            gameObject.SetActive(true);
        }

        //隐藏
        public virtual void OnHide()
        {

            gameObject.SetActive(false);
        }
        
        //关闭界面（销毁）
        public virtual void Close()
        {
            UIModule.Instance.CloseUI(gameObject.name);
        }
        
        public UIEventTrigger Register(string name)
        {
            Transform tf = transform.Find(name);
            return UIEventTrigger.Get(tf.gameObject);
        }
    }
    
    public class UIEventTrigger : MonoBehaviour, IPointerClickHandler
    {
        //这是一个公共的委托，它接受两个参数，一个是被点击的游戏对象，另一个是关于点击事件的数据。
        public Action<GameObject, PointerEventData> onClick;

        //用于获取或添加 UIEventTrigger 组件
        public static UIEventTrigger Get(GameObject obj)
        {
            UIEventTrigger trigger = obj.GetComponent<UIEventTrigger>();
            if (trigger == null)
            {
                trigger = obj.AddComponent<UIEventTrigger>();
            }
            return trigger;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //这是 IPointerClickHandler 接口的方法，当 UI 元素被点击时，它将被调用。
            if (onClick != null) onClick(gameObject, eventData);
        }
    }
}
