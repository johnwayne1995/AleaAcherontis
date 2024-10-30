using Interfaces;
using Managers;
using Modules;
namespace Fsm.FightStages
{
    public class FightStage_EnemyTurnSettlement : FightStageBase
    {
        private FightManager _fightManager;
        
        public FightStage_EnemyTurnSettlement(EFIGHT_STAGE stateType, IFsmController<EFIGHT_STAGE> controller) : base(stateType, controller)
        {
        }
        protected override void OnEnterStage(object e = null)
        {
            _fightManager = GameManagerContainer.Instance.GetManager<FightManager>();
            var enemyManager = GameManagerContainer.Instance.GetManager<EnemyManager>();
            enemyManager.DoAction();

            if (_fightManager.RoundOver())
            {
                STimer.Wait(0.5f, () =>
                {
                    _fightManager.ChangeState(EFIGHT_STAGE.Fail);
                });
                return;
            }
            
            if (_fightManager.CurHp > 0)
            {
                STimer.Wait(0.2f, () =>
                {
                    _fightManager.ChangeState(EFIGHT_STAGE.Player);
                });
            }
        }
        protected override void OnUpdateStage(float deltaTimes)
        {
            if (_fightManager.CurHp <= 0)
            {
                _fightManager.ChangeState(EFIGHT_STAGE.Fail);
            }
        }
        protected override void OnLeaveStage(object e = null)
        {
        }
    }
}
