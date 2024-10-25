using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Modules
{
    public class UIModule : AbstractModule<UIModule>
    {
        private Transform _canvasTf; //画布的变换组件
        private List<UIBase> _uiList; //存储加载过的界面的集合
        private Dictionary<string, UIBase> _uiDic;
        
        public override void Start()
        {
            base.Start();
            //找世界中的画布
            _canvasTf = GameObject.Find("GameMain/Canvas").transform;
            //初始化集合
            _uiList = new List<UIBase>();
            _uiDic = new Dictionary<string, UIBase>();
        }

        public UIBase ShowUI<T>(string uiName) where T : UIBase
        {
            UIBase ui = Find(uiName);
            if (ui == null)
            {
                //集合中没有 需要从Resources/UI文件夹加载
                GameObject obj = GameObject.Instantiate(Resources.Load("UI/" + uiName), _canvasTf) as GameObject;

                //改名字，默认实例化会加上（clone），所以得重命名
                obj.name = uiName;

                //添加需要的脚本
                ui = obj.AddComponent<T>();

                //添加到集合进行存储
                _uiList.Add(ui);
                _uiDic.Add(ui.name, ui);
            }

            ui.OnShow();
            return ui;
        }


        //隐藏
        public void HideUI(string uiName)
        {
            UIBase ui = Find(uiName);
            if (ui != null)
            {
                ui.OnHide();
            }
        }

        //关闭某个界面
        public void CloseUI(string uiName)
        {
            UIBase ui = Find(uiName);
            if (ui != null)
            {
                _uiList.Remove(ui);
                _uiDic.Remove(ui.name);
                GameObject.Destroy(ui.gameObject);
            }
        }

        //关闭所有界面
        public void CloseAllUI()
        {
            for (int i = _uiList.Count - 1; i >= 0; i--)
            {
                GameObject.Destroy(_uiList[i].gameObject);
            }
            _uiList.Clear(); //清空合集
            _uiDic.Clear();
        }


        //从集合中找到名字对应的界面脚本
        public UIBase Find(string uiName)
        {
            if (_uiDic.TryGetValue(uiName, out var uiBase))
            {
                return uiBase;
            }
            
            return null;
        }

        //获取某个界面的脚本
        public T GetUI<T>(string uiName) where T : UIBase
        {
            UIBase ui = Find(uiName);
            if (ui != null)
            {
                return ui.GetComponent<T>();
            }
            return null;
        }
    }
}
