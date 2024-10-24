namespace Interfaces
{
    /// <summary>
    /// 状态机控制器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFsmController<T>
    {
        void InitState();

        bool SwitchState(T stateType, object e);

        bool SwitchStateImmediately(T stateType, object e);

        void UpdateState(float deltaTimes);
    
        IFsmState<T> GetCurrentState();
    }
}
