using System;
using System.Collections.Generic;
using Modules;
    
namespace Managers
{
    public class JobManager : TGameManager<JobManager>
    {
        private Dictionary<Type, ObjectPool<DependentJob>> _jobsPool = new Dictionary<Type, ObjectPool<DependentJob>>();
        
        protected override void OnAwake()
        {
            InitJobMapping();
        }

        private void InitJobMapping()
        {
            _jobsPool = new Dictionary<Type, ObjectPool<DependentJob>>();
        }
        
        private void RegisterJob<T>() where T : DependentJob
        {
            _jobsPool.Add(typeof(T),new ObjectPool<DependentJob>(null,null, Activator.CreateInstance<T>));
        }
        
        public T CreateNewJob<T>() where T : DependentJob
        {
            Type type = typeof(T);
            if (!_jobsPool.ContainsKey(type))
            {
                throw new Exception($"unknown job type {type}");
            }
            
            return _jobsPool[type].Get() as T;
        }
        
        public void RecycleJob<T>(T job) where T : DependentJob
        {
            Type type = typeof(T);
            if (!_jobsPool.ContainsKey(type))
            {
                return;
            }
            
            job.Dispose();
            _jobsPool[type].Release(job);
        }

        public void RecycleJobList(ref List<DependentJob> jobList)
        {
            if(jobList == null || jobList.Count == 0)
                return;

            int jobCount = jobList.Count;
            for (int i = 0; i < jobCount; ++i)
            {
                RecycleJob(jobList[i]);
            }
            
            jobList.Clear();
        }
    }
}
