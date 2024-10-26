namespace Interfaces
{
    public interface IJob
    {
        /// <summary>
        /// 启动工作
        /// </summary>
        void Start();

        /// <summary>
        /// 暂停
        /// </summary>
        void Pause();

        /// <summary>
        /// 设置延迟(秒)
        /// </summary>
        void SetDelayTimes(float seconds);

        /// <summary>
        /// 继续
        /// </summary>
        void Continue();

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="deltaTime"></param>
        void UpdateJob(float deltaTime);

        /// <summary>
        /// 打断
        /// </summary>
        void Cancel();

        /// <summary>
        /// 重置
        /// </summary>
        void Reset();

        /// <summary>
        /// 销毁
        /// </summary>
        void Dispose();
    }
}
