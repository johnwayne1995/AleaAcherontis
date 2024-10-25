using Interfaces;
using Modules;
using UI;
namespace Fsm.FightStages
{
    public class FightStage_Win : FightStageBase
    {

        public FightStage_Win(EFIGHT_STAGE stateType, IFsmController<EFIGHT_STAGE> controller) : base(stateType, controller)
        {
        }
        protected override void OnEnterStage(object e = null)
        {
            UIModule.Instance.ShowUI<WinUI>("WinUI");
        }
        protected override void OnUpdateStage(float deltaTimes)
        {
        }
        protected override void OnLeaveStage(object e = null)
        {
        }
    }
}
