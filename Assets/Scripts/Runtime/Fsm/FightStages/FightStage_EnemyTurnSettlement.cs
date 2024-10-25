using Interfaces;
namespace Fsm.FightStages
{
    public class FightStage_EnemyTurnSettlement : FightStageBase
    {

        public FightStage_EnemyTurnSettlement(EFIGHT_STAGE stateType, IFsmController<EFIGHT_STAGE> controller) : base(stateType, controller)
        {
        }
        protected override void OnEnterStage(object e = null)
        {
        }
        protected override void OnUpdateStage(float deltaTimes)
        {
        }
        protected override void OnLeaveStage(object e = null)
        {
        }
    }
}
