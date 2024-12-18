﻿using Fsm;
using Interfaces;
using Managers;
using Modules;
using UI;
namespace GameStages
{
    public class GameStage_GameMain : GameStageBase
    {

        public GameStage_GameMain(EGAME_STAGE stateType, IFsmController<EGAME_STAGE> controller) : base(stateType, controller)
        {
        }
        protected override void OnEnterStage(object e = null)
        {
            GameManagerContainer.Instance.ReEnterGame();
            UIModule.Instance.ShowUI<FightUI>("FightUI");
            var fightMgr = GameManagerContainer.Instance.GetManager<FightManager>();
            fightMgr.SetCardGroup(null);
            var matchLevelManager = GameManagerContainer.Instance.GetManager<MatchLevelManager>();
            var table = matchLevelManager.GetMatchLevelTable();
            string path = table[matchLevelManager.curRoom].EnemyConfig;
            fightMgr.SetEnemy(path);
            fightMgr.StartFight();
        }
        
        protected override void OnLeaveStage(object e = null)
        {
        }
    }
}
