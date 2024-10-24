using System.Collections.Generic;
using Fsm;
namespace FightStages
{
    public class FightStageController : FsmStateControllerBase<EFIGHT_STAGE>
    {
        public class FightStageTypeComparer : IEqualityComparer<EFIGHT_STAGE>
        {
            public bool Equals(EFIGHT_STAGE x, EFIGHT_STAGE y)
            {
                return (int)x == (int)y;
            }

            public int GetHashCode(EFIGHT_STAGE x)
            {
                return (int)x;
            }
        }
        
        
        public override void InitState()
        {
            base.InitState(new FightStageTypeComparer());
        }
    }
}
