using Fsm;
using Interfaces;
using Managers;
using Modules;
using UI;

namespace GameStages
{
    public class GameStage_Start : GameStageBase
    {

        public GameStage_Start(EGAME_STAGE stateType, IFsmController<EGAME_STAGE> controller) : base(stateType, controller)
        {
        }
        protected override void OnEnterStage(object e = null)
        {
            UIModule.Instance.CloseAllUI();
            UIModule.Instance.ShowUI<StartUI>("StartUI");
        }
        
        protected override void OnLeaveStage(object e = null)
        {
        }
    }
}
