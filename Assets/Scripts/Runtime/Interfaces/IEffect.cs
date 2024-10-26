namespace Interfaces
{
    public interface IEffect
    {
        /// <summary>
        /// 启动工作
        /// </summary>
        void StartEffect();

        /// <summary>
        /// 移除效果
        /// </summary>
        void Dispose();
    }
}
