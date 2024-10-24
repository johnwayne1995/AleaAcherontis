using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Fsm
{
    public class FsmStateControllerBase<T> : IFsmController<T>
    {
        protected FsmStateBase<T> currentState;
        protected FsmStateBase<T> targetState;
        protected FsmStateBase<T> defaultState;
        protected FsmStateBase<T> lastState;
        protected bool bSwitching;
        protected IEqualityComparer<T> dicComparer;
        protected Dictionary<T, FsmStateBase<T>> allStates;

        public T CurrentStateType
        {
            get
            {
                if (currentState != null)
                {
                    return currentState.StateType;
                }
                else
                {
                    return default(T);
                }
            }
        }
        public IFsmState<T> GetCurrentState()
        {
            return currentState;
        }

        public virtual void InitState()
        {
            InitState(null);
        }

        public virtual void InitState(IEqualityComparer<T> dicComparer = null)
        {
            var type = typeof(T);

            if (typeof(System.ValueType).IsAssignableFrom(typeof(T)) &&
                !typeof(System.IEquatable<T>).IsAssignableFrom(typeof(T)) && dicComparer == null)
            {
                Debug.LogError($"{type.ToString()} 是没有实现IEquatable值类型, 需要一个IEqualityComparer，避免GC！");
                dicComparer = EqualityComparer<T>.Default;
            }

            this.dicComparer = dicComparer;
            allStates = new Dictionary<T, FsmStateBase<T>>(dicComparer);
        }

        public virtual void SetDefault(T stateType)
        {
            if (allStates.ContainsKey(stateType))
                defaultState = allStates[stateType];
        }

        public void AddState(T stateType, FsmStateBase<T> state)
        {
            if (allStates.ContainsKey(stateType))
            {
                allStates.Remove(stateType);
            }
            allStates[stateType] = state;
        }

        public IFsmState<T> GetState(T stateType)
        {
            if (allStates.ContainsKey(stateType))
                return allStates[stateType];

            return null;
        }

        public virtual void UpdateState(float dt)
        {
            if (targetState != null)
            {
                try
                {
                    bSwitching = true;
                    if (currentState != null)
                    {
                        lastState = currentState;
                        currentState.LeaveState();
                    }
                    var tar = targetState;
                    currentState = allStates[tar.StateType];
                    targetState = null;
                    currentState.EnterState(tar.UserData);
                }
                finally
                {
                    bSwitching = false;
                }
                if (currentState != null)
                    currentState.UpdateState(dt);
            }
            else if (currentState != null)
            {
                currentState.UpdateState(dt);
            }
            else
            {
                if (defaultState != null)
                {
                    currentState = defaultState;
                    currentState.EnterState();
                }
            }
        }
        public bool LeaveCurrentState()
        {
            if (currentState != null)
            {
                currentState.LeaveState();
                currentState = null;
                return true;
            }
            return false;
        }
        public bool IsInXState(T stateType) { return dicComparer.Equals(CurrentStateType, stateType); }
        public bool IsInXState<FS, E>() where FS : FsmStateBase<E>
        {
            if (currentState == null) return false;
            return currentState is FS;
        }

        public bool IsExsitState(T stateType)
        {
            if (allStates.ContainsKey(stateType))
                return true;

            return false;
        }

        public virtual bool SwitchState(T stateType, object e = null)
        {
            if (bSwitching)
            {
                Debug.LogError("fsm is switching state, please don't do that again!");
                return false;
            }
            targetState = allStates[stateType];
            targetState.SetUserData(e);
            return true;
        }
        public virtual bool SwitchStateImmediately(T stateType, object e = null)
        {
            if (bSwitching)
            {
                Debug.LogError("fsm is switching state, please don't do that again~!~!~!");
                return false;
            }
            SwitchState(stateType, e);
            UpdateState(0);
            return true;
        }
        protected bool IsTransitionValid(T stateType)
        {
            if (!allStates.ContainsKey(stateType))
            {
                Debug.LogWarning($"this fsm does't contians type:{stateType.ToString()} ");
                return false;
            }
            if (currentState != null)
            {
                if (!currentState.CanTransTo(stateType))
                    return false;
            }
            return true;
        }

        public virtual bool TrySwitchStateImmediately(T stateType, object e = null)
        {
            if (IsTransitionValid(stateType))
                SwitchStateImmediately(stateType, e);
            return true;
        }

        public virtual void ClearAllStates()
        {
            currentState = null;
            if (allStates != null)
            {
                allStates.Clear();
                allStates = null;
            }
        }
    }
}
