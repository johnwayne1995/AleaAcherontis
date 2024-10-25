using Interfaces;
using Modules;
using UI;
namespace Fsm.FightStages
{
    public class FightStage_Fail : FightStageBase
    {

        public FightStage_Fail(EFIGHT_STAGE stateType, IFsmController<EFIGHT_STAGE> controller) : base(stateType, controller)
        {
        }
        protected override void OnEnterStage(object e = null)
        {
            UIModule.Instance.ShowUI<FailUI>("FailUI");
        }
        protected override void OnUpdateStage(float deltaTimes)
        {
        }
        protected override void OnLeaveStage(object e = null)
        {
        }
    }
}
