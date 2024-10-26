using System;
using System.Collections.Generic;
using Interfaces;
using Modules;
using UnityEngine.Pool;

namespace Managers
{
    /// <summary>
    /// 依次执行  一个失败就是失败
    /// </summary>
    public class SequenceMasterJob : DependentJob, ITypeJob, IJobResetOperation
    {
        private List<DependentJob> _queueJobs;
        private Queue<DependentJob> _usingJob;
        
        private DependentJob _executingJob;

        public SequenceMasterJob()
        {
            _queueJobs = new List<DependentJob>();
            _usingJob = new Queue<DependentJob>();
        }

        public void SetDependenies(ref List<DependentJob> jobs)
        {
            if(jobs == null)
                return;

            foreach (var job in jobs)
            {
                if(job == null)
                    continue;
                
                _queueJobs.Add(job);
                _usingJob.Enqueue(job);
            }
            
            jobs.Clear();
            ListPool<DependentJob>.Release(jobs);
        }
        
        protected override void OnResetJob()
        {
            _executingJob = null;
            _usingJob.Clear();
            
            for(int i = 0; i < _queueJobs.Count; ++i)
            {
                if(_queueJobs[i] == null)
                    continue;

                if (i == 0)
                {
                    _queueJobs[i].Reset();
                }
                
                _usingJob.Enqueue(_queueJobs[i]);
            }
            
            CheckDependencies();
        }

        public virtual void ResetCurSubJob(int jobIndex, float fDelayTimes = 0f)
        {
            if(jobIndex < 0 || jobIndex >= _queueJobs.Count)
                return;

            _executingJob = _queueJobs[jobIndex];
            if (fDelayTimes > 0)
            {
                _executingJob.SetDelayTimes(fDelayTimes);
            }
        }

        protected override void OnExecuteJob()
        {
            TryNextJob();
        }

        private bool TryNextJob()
        {
            if (_usingJob.Count == 0)
                return false;
            
            if (_executingJob == null)
            {
                _executingJob = _usingJob.Dequeue();
            }

            _executingJob.jobCompletedEvent = 
                (XJobCompletedHandler) Delegate.Combine(_executingJob.jobCompletedEvent, new XJobCompletedHandler(this.OnDependencyJobComplete));
            
            _executingJob.Start();
            return true;
        }

        protected bool CheckEmptyJobList()
        {
            return _usingJob.Count == 0;
        }
        
        protected override void OnUpdateJob(float deltaTime)
        {
            if (Status != EJOB_STATUS.RESETING)
            {
                if (_executingJob == null)
                {
                    if (!TryNextJob())
                    {
                        MarkJobSuccess();
                        return;
                    }
                }
            }
     
            if(_executingJob == null)
                return;
            
            _executingJob.UpdateJob(deltaTime);
        }

        public override void Dispose()
        {
            base.Dispose();

            if (_executingJob != null)
            {
                _executingJob.jobCompletedEvent = 
                    (XJobCompletedHandler) Delegate.Remove(_executingJob.jobCompletedEvent, new XJobCompletedHandler(this.OnDependencyJobComplete));
                _executingJob = null;
            }
            
            if (_usingJob != null)
            {
                while (_usingJob.Count > 0)
                {
                    _queueJobs.Add(_usingJob.Dequeue());
                }
            }

            GameManagerContainer.Instance.GetManager<JobManager>().RecycleJobList(ref _queueJobs);
        }

        private void OnDependencyJobComplete(DependentJob job)
        {
            if(_executingJob != job)
                return;
            
            GameManagerContainer.Instance.GetManager<JobManager>().RecycleJob(_executingJob);
            _executingJob = null;
            job.jobCompletedEvent = (XJobCompletedHandler) Delegate.Remove(job.jobCompletedEvent,
                new XJobCompletedHandler(this.OnDependencyJobComplete));
            
            if (job.Status != EJOB_STATUS.SUCCESS)
            {
                SetJobStatus(EJOB_STATUS.RESETING);
                OnSubJobFailed(job);
            }
            else
            {
                OnSubJobComplete(job);
            }
        }

        protected virtual void OnSubJobComplete(DependentJob job)
        {
            
        }

        protected virtual void OnSubJobFailed(DependentJob job)
        {
            
        }
    }
}
