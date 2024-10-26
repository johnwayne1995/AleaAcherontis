namespace Managers
{
    public partial class DependentJob
    {
        /// <summary>
        /// 状态
        /// </summary>
        public enum EJOB_STATUS
        {
            CREATED,    //创建完成
            WAITING,    //等待执行
            EXECUTING,  //执行中
            RESETING,   //重置中
            SUCCESS,    //执行成功
            FAILED,     //执行失败
            SKIPPED,    //忽略
            CANCELLED,  //取消
            RECYCLE,
        }

        public delegate void XJobCompletedHandler(DependentJob job);

        public XJobCompletedHandler jobCompletedEvent;
        public event XJobCompletedHandler JobCompletedEvent
        {
            add => jobCompletedEvent += value;
            remove
            {
                if (jobCompletedEvent != null) 
                    jobCompletedEvent -= value;
            }
        }

        /*public XJobCompletedHandler jobFailedEvent;
        public event XJobCompletedHandler JobFailedEvent
        {
            add => jobFailedEvent += value;
            remove
            {
                if (jobFailedEvent != null) 
                    jobFailedEvent -= value;
            }
        }*/

        protected void SetJobStatus(EJOB_STATUS jobStatus)
        {
            Status = jobStatus;
            
            //D.Error($"{this} SetJobStatus {jobStatus}");
        }
    }
}
