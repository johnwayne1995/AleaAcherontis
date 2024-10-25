﻿using Interfaces;
using Managers;
using Modules;
using UI;
namespace Fsm.FightStages
{
    public class FightStage_PlayerTurnSettlement : FightStageBase
    {

        public FightStage_PlayerTurnSettlement(EFIGHT_STAGE stateType, IFsmController<EFIGHT_STAGE> controller) : base(stateType, controller)
        {
        }
        protected override void OnEnterStage(object e = null)
        {
           //todo 展示伤害

           var enemyManager = GameManagerContainer.Instance.GetManager<EnemyManager>();
           var fightCardMgr = GameManagerContainer.Instance.GetManager<FightCardManager>();

           
           enemyManager.Hit(fightCardMgr.GetCurHandsDamage());
           
           STimer.Wait(1.5f, () =>
           {
               var fightUi = UIModule.Instance.GetUI<FightUI>("FightUI");
               fightUi.RemoveAllSendCard();
           });
        }
        protected override void OnUpdateStage(float deltaTimes)
        {
        }
        protected override void OnLeaveStage(object e = null)
        {
        }
    }
}
