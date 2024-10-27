using GCommon.Excel;
using Managers;
using Modules;
using UnityEngine;

namespace CardGame
{
    public class GameMain : MonoBehaviour
    {
        private ModuleMgr _moduleMgr;       
        private int _pauseCnt = 0;
        
        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            _moduleMgr = ModuleMgr.Ins;
            _moduleMgr.AddModule(UIModule.Instance);
            _moduleMgr.AddModule(TimerModule.Instance);
            _moduleMgr.AddModule(GameManagerContainer.Instance);
            _moduleMgr.AddModule(GameStageModule.Instance);
            _moduleMgr.Start();
        }
        
        public void Update()
        {
            _moduleMgr.Update();
        }

        public void LateUpdate()
        {
            _moduleMgr.LateUpdate();
        }

        public void FixedUpdate()
        {
            _moduleMgr.FixedUpdate();
        }
        
        public void Pause()
        {
            _pauseCnt++;
            if (_pauseCnt > 1)
                return;
            _moduleMgr.Pause();
        }

        public void Continue()
        {
            _pauseCnt--;
            if (_pauseCnt > 0)
                return;
            _moduleMgr.Continue();
        }
        
        public void Dispose()
        {
            _moduleMgr.Dispose();
        }
    }
}
