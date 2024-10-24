﻿using Fsm;
using Managers;
using Modules;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class StartUI : UIBase
    {
        private void Awake()
        {
            Register("startBtn").onClick = OnStartGameBtnClick;
        }

        public override void OnShow()
        {
            base.OnShow();
            var audioMgr = GameManagerContainer.Instance.GetManager<AudioManager>();
            audioMgr.PlayBgm("bgm1", true);
        }

        private void OnStartGameBtnClick(GameObject obj, PointerEventData pData)
        {
            //关闭login界面
            Close();
            GameStageModule.Instance.SwitchStage(EGAME_STAGE.GameMain);
        }
    }
}
