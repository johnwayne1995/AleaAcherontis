namespace Interfaces
{
    public interface IJobResetOperation
    {
        /// <summary>
        /// 重置当前失败的子job
        /// </summary>
        /// <param name="jobIndex"></param>
        void ResetCurSubJob(int jobIndex, float fDelayTimes = 0f);
    }
}
