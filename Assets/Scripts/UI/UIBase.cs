using Managers;
using Modules;
using UnityEngine;

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
    }
}
