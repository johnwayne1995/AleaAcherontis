using System;
using System.Collections.Generic;
using Interfaces;
using Modules;
using UnityEngine.Pool;
namespace Managers
{
    /// <summary>
    /// 并行Job
    /// </summary>
    public class ParallelMasterJob : DependentJob, ITypeJob
    {
        protected List<DependentJob> _listDependencies;
        
        public ParallelMasterJob(string strJobName) 
            : base(strJobName)
        {
            _listDependencies = new List<DependentJob>();
        }
        
        public void SetDependenies(ref List<DependentJob> jobs)
        {
            if(jobs == null)
                return;

            foreach (var job in jobs)
            {
                if(job == null)
                    continue;
                
                _listDependencies.Add(job);
            }
            
            jobs.Clear();
            ListPool<DependentJob>.Release(jobs);
        }

        public void SetDependentJob(DependentJob job)
        {
            if(job == null)
                return;
            
            _listDependencies.Add(job);
        }

        protected override void OnExecuteJob()
        {
            foreach (var jobItem in _listDependencies)
            {
                jobItem.jobCompletedEvent = 
                    (XJobCompletedHandler) Delegate.Combine(jobItem.jobCompletedEvent, new XJobCompletedHandler(this.OnDependencyJobComplete));
                
                jobItem.Start();
            }
        }
        
        protected override void OnUpdateJob(float deltaTime)
        {
            foreach (var jobItem in _listDependencies)
            {
                jobItem.UpdateJob(deltaTime);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            
            if (Status == EJOB_STATUS.WAITING)
            {
                foreach (var jobItem in _listDependencies)
                {
                    jobItem.jobCompletedEvent = (XJobCompletedHandler) Delegate.Remove(jobItem.jobCompletedEvent,
                        new XJobCompletedHandler(this.OnDependencyJobComplete));
                }
            }

            GameManagerContainer.Instance.GetManager<JobManager>().RecycleJobList(ref _listDependencies);
            ListPool<DependentJob>.Release(_listDependencies);
        }

        private void OnDependencyJobComplete(DependentJob job)
        {
            if(job == null)
                return;

            job.jobCompletedEvent = (XJobCompletedHandler) Delegate.Remove(job.jobCompletedEvent,
                new XJobCompletedHandler(this.OnDependencyJobComplete));

            foreach (var subJob in _listDependencies)
            {
                if(subJob.Status != EJOB_STATUS.SUCCESS)
                    return;
            }
            MarkJobSuccess();
        }
    }
}
