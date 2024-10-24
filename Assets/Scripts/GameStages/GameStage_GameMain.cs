using Fsm;
using Interfaces;
using Modules;
using UI;
namespace GameStages
{
    public class GameStage_GameMain : GameStageBase
    {

        public GameStage_GameMain(EGAME_STAGE stateType, IFsmController<EGAME_STAGE> controller) : base(stateType, controller)
        {
        }
        protected override void OnEnterStage(object e = null)
        {
            UIModule.Instance.ShowUI<MainUI>("MainUI");
        }
        protected override void OnLeaveStage(object e = null)
        {
        }
    }
}
