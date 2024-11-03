using System.Collections.Generic;
using Fsm;
using Managers;
using Modules;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class LevelChoiceUI : UIBase
    {
        private Transform _LevelPanel;
        private List<Leveltem> LevelList;

        private void Awake()
        {
            // Register("Button_challenge1").onClick = OnStartGameBtnClick;
            // Register("Button_challenge2").onClick = OnStartGameBtnClick;
            // Register("Button_challenge3").onClick = OnStartGameBtnClick;
            // Register("Button_skip1").onClick = OnSkipGameBtnClick;
            // Register("Button_skip2").onClick = OnSkipGameBtnClick;
            Register("Button_skip3").onClick = OnSkipGameBtnClick;
            _LevelPanel = Find("LevelPanel");
            _LevelPanel.gameObject.SetActive(false);
        }

        public override void OnShow()
        {
            base.OnShow();
            var audioMgr = GameManagerContainer.Instance.GetManager<AudioManager>();
            audioMgr.PlayBgm("bgm1", true);
            var matchLevelManager = GameManagerContainer.Instance.GetManager<MatchLevelManager>();
            var table = matchLevelManager.GetMatchLevelTable();
            LevelList = new List<Leveltem>();
            foreach (var item in table.RecordList)
            {
                if (item.RoomLayer == table[matchLevelManager.curRoom].RoomLayer)
                {
                    var level = Instantiate(_LevelPanel.gameObject, _LevelPanel.parent).GetComponent<Leveltem>();
                    bool isCurLevel = item.RoomID == matchLevelManager.curRoom;
                    level.Side.gameObject.SetActive(isCurLevel);
                    
                    if (item.RoomID > matchLevelManager.curRoom)
                    {
                        level.Locked.SetActive(true);
                    }

                    if (item.RoomID < matchLevelManager.curRoom)
                    {
                        level.Done.SetActive(true);
                    }
                    
                    level.ChallengeBtn.onClick.AddListener(OnStartGameBtnClick);
                    level.SkipBtn.onClick.AddListener(OnSkipGameBtnClick);
                    level.Name.text = GetEnemyName(item.EnemyType);
                    level.Hp.text = "血量："+item.EnemyBlood.ToString();
                    level.gameObject.SetActive(true);
                    level.DisableSkipBtn.SetActive(!item.CanSkip);

                    LevelList.Add(level);
                }

            }
            // _CurRoomID.text = "RoomID: " + matchLevelManager.curRoom.ToString();

        }

        private string GetEnemyName(int itemEnemyType)
        {
            switch (itemEnemyType)
            {
                case 0:
                    return "普通怪";
                    break;
                case 1:
                    return "NPC";
                    break;
                case 2:
                    return "Boss";
                    break;
            }

            return default;
        }

        private void OnStartGameBtnClick()
        {
            //关闭login界面 , 
            Close();
            GameStageModule.Instance.SwitchStage(EGAME_STAGE.GameMain);
        }

        private void OnStartGameBtnClick(GameObject obj, PointerEventData pData)
        {
            //关闭login界面 , 
            Close();
            GameStageModule.Instance.SwitchStage(EGAME_STAGE.GameMain);
        }
        

        private void OnSkipGameBtnClick()
        {
            //关闭login界面 , 
            Close();
            GameStageModule.Instance.SwitchStage(EGAME_STAGE.MatchRoom);
            GameManagerContainer.Instance.GetManager<MatchLevelManager>().SkipCurRoom();
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
