using Fsm;
using Interfaces;
namespace GameStages
{
    public class GameStage_GameMain : GameStageBase
    {

        public GameStage_GameMain(EGAME_STAGE stateType, IFsmController<EGAME_STAGE> controller) : base(stateType, controller)
        {
        }
        protected override void OnEnterStage(object e = null)
        {
        }
        protected override void OnLeaveStage(object e = null)
        {
        }
    }
}
