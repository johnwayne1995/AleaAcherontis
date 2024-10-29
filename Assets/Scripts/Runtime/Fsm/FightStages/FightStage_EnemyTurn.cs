using Interfaces;
using Modules;
using UI;
namespace Fsm.FightStages
{
    public class FightStage_EnemyTurn : FightStageBase
    {

        public FightStage_EnemyTurn(EFIGHT_STAGE stateType, IFsmController<EFIGHT_STAGE> controller) : base(stateType, controller)
        {
        }
        protected override void OnEnterStage(object e = null)
        {
            var fightUi = UIModule.Instance.GetUI<FightUI>("FightUI");
            fightUi.FlushRoundCount();
            
            //todo 播放敌人回合UI
            STimer.Wait(0.5f, () =>
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
