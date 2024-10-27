using Fsm;
using Interfaces;
using Modules;
using UI;

namespace GameStages
{
    public class GameStage_MatchRoom : GameStageBase
    {
        public GameStage_MatchRoom(EGAME_STAGE stateType, IFsmController<EGAME_STAGE> controller) : base(stateType, controller)
        {
        }
        protected override void OnEnterStage(object e = null)
        {
            UIModule.Instance.CloseAllUI();
            UIModule.Instance.ShowUI<MatchRoomUI>("MatchRoomUI");
        }
        
        protected override void OnLeaveStage(object e = null)
        {
        }
    }
}