namespace Interfaces
{
    /// <summary>
    /// 状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFsmState<T>
    {
        /// <summary>
        /// 状态进入
        /// </summary>
        /// <param name="e"></param>
        void EnterState(object e);

        /// <summary>
        /// 状态轮询 优化期可能直接废除 自己注册
        /// </summary>
        /// <param name="deltaTimes"></param>
        void UpdateState(float deltaTimes);
     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        void LeaveState(object e);
    }
}
