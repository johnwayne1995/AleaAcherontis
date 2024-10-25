using Interfaces;
using Modules;
namespace Fsm.FightStages
{
    public class FightStage_EnemyTurn : FightStageBase
    {

        public FightStage_EnemyTurn(EFIGHT_STAGE stateType, IFsmController<EFIGHT_STAGE> controller) : base(stateType, controller)
        {
        }
        protected override void OnEnterStage(object e = null)
        {
            //todo 播放敌人回合UI
            STimer.Wait(1, () =>
            {
                controller.SwitchState(EFIGHT_STAGE.EnemyTurnSettlement, null);
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
