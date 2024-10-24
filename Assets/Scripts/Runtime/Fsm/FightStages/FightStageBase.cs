using Interfaces;
namespace Fsm.FightStages
{
    public abstract class FightStageBase : FsmStateBase<EFIGHT_STAGE>
    {
        protected FightStageBase(EFIGHT_STAGE stateType, IFsmController<EFIGHT_STAGE> controller) : base(stateType, controller)
        {
        }
        
        public override void EnterState(object e = null)
        {
            OnEnterStage(e);
        }

        public override void UpdateState(float deltaTimes)
        {
            OnUpdateStage(deltaTimes);
        }

        public override void LeaveState(object e = null)
        {
            OnLeaveStage(e);
        }

        protected abstract void OnEnterStage(object e = null);

        protected abstract void OnUpdateStage(float deltaTimes);
        
        protected abstract void OnLeaveStage(object e = null);
    }
}
