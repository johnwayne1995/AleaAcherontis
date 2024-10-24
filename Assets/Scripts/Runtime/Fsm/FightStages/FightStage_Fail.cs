﻿using Interfaces;
namespace Fsm.FightStages
{
    public class FightStage_Fail : FightStageBase
    {

        public FightStage_Fail(EFIGHT_STAGE stateType, IFsmController<EFIGHT_STAGE> controller) : base(stateType, controller)
        {
        }
        protected override void OnEnterStage(object e = null)
        {
            throw new System.NotImplementedException();
        }
        protected override void OnUpdateStage(float deltaTimes)
        {
            throw new System.NotImplementedException();
        }
        protected override void OnLeaveStage(object e = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
