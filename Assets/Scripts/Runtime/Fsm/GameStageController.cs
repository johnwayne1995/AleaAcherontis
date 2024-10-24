using System.Collections.Generic;
namespace Fsm
{
    public class GameStageController : FsmStateControllerBase<EGAME_STAGE>
    {
        public class GameStageTypeComparer : IEqualityComparer<EGAME_STAGE>
        {
            public bool Equals(EGAME_STAGE x, EGAME_STAGE y)
            {
                return (int)x == (int)y;
            }

            public int GetHashCode(EGAME_STAGE x)
            {
                return (int)x;
            }
        }
        
        
        public override void InitState()
        {
            base.InitState(new GameStageTypeComparer());
        }
    }
}
