using Interfaces;
namespace Fsm
{
    public class FsmStateBase<T> : IFsmState<T>
    {
        protected bool bStateEnd;
        protected readonly IFsmController<T> controller;
    
        protected readonly T stateType;
        public T StateType
        {
            get { return stateType; }
        }
    
        public object UserData
        {
            get;
            private set;
        }

        public FsmStateBase(T stateType, IFsmController<T> controller)
        {
            this.stateType = stateType;
            this.controller = controller;
            this.bStateEnd = false;
        }
        public void SetUserData(object userData)
        {
            this.UserData = userData;
        }

        public bool IsStateEnd()
        {
            return bStateEnd;
        }
        public virtual  void EnterState(object e = null)
        {
        
        }
        public virtual void UpdateState(float deltaTimes)
        {
        
        }
        public virtual void LeaveState(object e = null)
        {
        
        }
        public virtual bool CanTransTo(T stateType)
        {
            return true;
        }
    }
}
