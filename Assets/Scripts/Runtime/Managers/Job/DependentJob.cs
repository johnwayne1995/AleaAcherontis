using Interfaces;
namespace Managers
{
    /// <summary>
    /// 基础Job 
    /// </summary>
    public partial class DependentJob : IJob
    {
        protected float _fDelayStartTimes = 0f;

        public DependentJob()
        {
            CommonConstruction();
        }

        public DependentJob(string strJobName)
        {
            JobName = strJobName;
            CommonConstruction();
        }

        public virtual void SerializeParam(object param)
        {
            
        }

        public virtual void Start()
        {
            SetJobStatus(EJOB_STATUS.WAITING);

            if (_fDelayStartTimes <= 0)
            {
                CheckDependencies();
            }
        }

        public void Pause()
        {
            SetJobStatus(EJOB_STATUS.WAITING);
        }

        public void SetDelayTimes(float seconds)
        {
            _fDelayStartTimes = seconds;
        }

        public void Continue()
        {
            SetJobStatus(EJOB_STATUS.EXECUTING);
        }

        public virtual void UpdateJob(float deltaTime)
        {
            if (_fDelayStartTimes > 0)
            {
                _fDelayStartTimes -= deltaTime;
                if (_fDelayStartTimes <= 0)
                {
                    CheckDependencies();
                }
                return;
            }
            
            if(Status != EJOB_STATUS.EXECUTING && Status != EJOB_STATUS.RESETING)
                return;
            
            OnUpdateJob(deltaTime);
        }

        public void Cancel()    
        {
            if(Status == EJOB_STATUS.FAILED)
                return;
            
            SetJobStatus(EJOB_STATUS.CANCELLED);
            OnBreakJob();
        }

        public void Reset()
        {
            SetJobStatus(EJOB_STATUS.EXECUTING);
            OnResetJob();
        }

        public virtual void Dispose()
        {
            SetJobStatus(EJOB_STATUS.RECYCLE);
            OnCleanJob();
        }
    }
}
