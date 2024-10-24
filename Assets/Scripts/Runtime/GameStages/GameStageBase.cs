using Fsm;
using Interfaces;
namespace GameStages
{
    public abstract class GameStageBase : FsmStateBase<EGAME_STAGE>
    {
        public GameStageBase(EGAME_STAGE stateType, IFsmController<EGAME_STAGE> controller)
            : base(stateType, controller)
        {
            
        }

        public override void EnterState(object e = null)
        {
            OnEnterStage(e);
        }
        
        public override void LeaveState(object e = null)
        {
            OnLeaveStage(e);
        }

        protected abstract void OnEnterStage(object e = null);

        protected abstract void OnLeaveStage(object e = null);
    }
}
