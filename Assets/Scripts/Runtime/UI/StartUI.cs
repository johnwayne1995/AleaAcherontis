using Fsm;
using Managers;
using Modules;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class StartUI : UIBase
    {
        private void Awake()
        {
            transform.Find("startBtn").GetComponent<Button>().onClick.AddListener(OnStartGameBtnClick);
        }

        public override void OnShow()
        {
            base.OnShow();
            var audioMgr = GameManagerContainer.Instance.GetManager<AudioManager>();
            audioMgr.PlayBgm("bgm1", true);
        }

        private void OnStartGameBtnClick()
        {
            //关闭login界面
            Close();
            GameStageModule.Instance.SwitchStage(EGAME_STAGE.MatchRoom);
        }
    }
}
