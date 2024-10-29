using Interfaces;
using Managers;
using Modules;
namespace Fsm.FightStages
{
    public class FightStage_PlayerTurn : FightStageBase
    {

        public FightStage_PlayerTurn(EFIGHT_STAGE stateType, IFsmController<EFIGHT_STAGE> controller) : base(stateType, controller)
        {
        }
        protected override void OnEnterStage(object e = null)
        {
            var fightManager = GameManagerContainer.Instance.GetManager<FightManager>();
            fightManager.EnterNewRound();
            
            var enemyManager = GameManagerContainer.Instance.GetManager<EnemyManager>();
            enemyManager.DoAction(false);
            
            //todo 展示回合UI
            //结束后切换到抽卡

            STimer.Wait(1, () =>
            {
                fightManager.ChangeState(EFIGHT_STAGE.LoadCard);
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
