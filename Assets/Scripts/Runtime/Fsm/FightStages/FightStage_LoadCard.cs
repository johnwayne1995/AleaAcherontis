using Interfaces;
using Modules;
using UI;
namespace Fsm.FightStages
{
    public class FightStage_LoadCard : FightStageBase
    {

        public FightStage_LoadCard(EFIGHT_STAGE stateType, IFsmController<EFIGHT_STAGE> controller) : base(stateType, controller)
        {
        }
        protected override void OnEnterStage(object e = null)
        {
            var fightUi = UIModule.Instance.GetUI<FightUI>("FightUI");
            fightUi.CreateCardItem(true);
        }
        
        protected override void OnUpdateStage(float deltaTimes)
        {
        }
        protected override void OnLeaveStage(object e = null)
        {
        }
    }
}
