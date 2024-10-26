namespace Managers
{
    public partial class DependentJob
    {
        /// <summary>
        /// job 名称
        /// </summary>
        public string JobName { get; private set; }

        /// <summary>
        /// 是否必须成功
        /// </summary>
        public bool SkipOnFailure { get; set; }

        /// <summary>
        /// job 当前状态
        /// </summary>
        public EJOB_STATUS Status { get; protected set; }

        private void CommonConstruction()
        {
            SkipOnFailure = false;
            SetJobStatus(EJOB_STATUS.CREATED);
        }
        
        protected virtual void CheckDependencies()
        {
            SetJobStatus(EJOB_STATUS.EXECUTING);
            OnExecuteJob();
        }

        protected void MarkJobSuccess()
        {
            if(Status >= EJOB_STATUS.SUCCESS)
                return;

            SetJobStatus(EJOB_STATUS.SUCCESS);
            if (jobCompletedEvent != null)
            {
                jobCompletedEvent(this);
            }
            
            OnJobComplete();
        }

        protected void MarkJobFailed(string reason = "")
        {
            OnJobFailed();
            SetJobStatus(EJOB_STATUS.FAILED);
            
            if (SkipOnFailure)//不是必须完成的Job
            {
                SetJobStatus(EJOB_STATUS.SKIPPED);
                return;
            }
            
            if (jobCompletedEvent != null)
            {
                jobCompletedEvent(this);
            }
        }
    }
}
