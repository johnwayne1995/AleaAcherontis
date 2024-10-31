using Config;
using Interfaces;
using Managers;
using Modules;
using UI;
using UnityEngine;
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
