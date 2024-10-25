using Interfaces;
using Managers;
using Modules;
namespace Fsm.FightStages
{
    public class FightStage_EnemyTurnSettlement : FightStageBase
    {

        public FightStage_EnemyTurnSettlement(EFIGHT_STAGE stateType, IFsmController<EFIGHT_STAGE> controller) : base(stateType, controller)
        {
        }
        protected override void OnEnterStage(object e = null)
        {
            var enemyManager = GameManagerContainer.Instance.GetManager<EnemyManager>();
            enemyManager.DoAction();
        }
        protected override void OnUpdateStage(float deltaTimes)
        {
        }
        protected override void OnLeaveStage(object e = null)
        {
        }
    }
}
