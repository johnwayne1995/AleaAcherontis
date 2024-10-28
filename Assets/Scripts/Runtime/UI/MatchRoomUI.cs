using Fsm;
using Managers;
using Modules;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class MatchRoomUI : UIBase
    {
        private Text _EnemyTypeText;
        private Text _CurRoomID;

        private void Awake()
        {
            Register("Battle").onClick = OnStartGameBtnClick;
            Register("Skip").onClick = OnSkipGameBtnClick;
            _EnemyTypeText = transform.Find("Enemy").GetComponentInChildren<Text>();
            _CurRoomID = transform.Find("CurRoomID").GetComponentInChildren<Text>();
        }

        public override void OnShow()
        {
            base.OnShow();
            var audioMgr = GameManagerContainer.Instance.GetManager<AudioManager>();
            audioMgr.PlayBgm("bgm1", true);
            var matchLevelManager = GameManagerContainer.Instance.GetManager<MatchLevelManager>();
            _CurRoomID.text = "RoomID: " + matchLevelManager.curRoom.ToString();
            switch (matchLevelManager.GetMatchLevelTable()[matchLevelManager.curRoom].EnemyType)
            {
                case 0:
                    _EnemyTypeText.text = "普通怪";
                    break;
                case 1:
                    _EnemyTypeText.text = "NPC";
                    break;
                case 2:
                    _EnemyTypeText.text = "Boss";
                    break;
                    
            }
        }

        private void OnStartGameBtnClick(GameObject obj, PointerEventData pData)
        {
            //关闭login界面 , 
            Close();
            GameStageModule.Instance.SwitchStage(EGAME_STAGE.GameMain);
        }
        
        private void OnSkipGameBtnClick(GameObject obj, PointerEventData pData)
        {
            //关闭login界面 , 
            Close();
            GameStageModule.Instance.SwitchStage(EGAME_STAGE.MatchRoom);
            GameManagerContainer.Instance.GetManager<MatchLevelManager>().SkipCurRoom();
        }
    }
}
