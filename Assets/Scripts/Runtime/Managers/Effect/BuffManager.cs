using System;
using System.Collections.Generic;
using Managers.Effects;
using Modules;

namespace Managers
{
    public class BuffManager : TGameManager<AudioManager>
    {
        private Dictionary<Type, ObjectPool<BuffBase>> _effectsPool = new Dictionary<Type, ObjectPool<BuffBase>>();
        
        protected override void OnAwake()
        {
            InitEffectMapping();
        }

        private void InitEffectMapping()
        {
            _effectsPool = new Dictionary<Type, ObjectPool<BuffBase>>();
        }
        
        protected override void OnBeforeDestroy()
        {
            foreach (var item in _effectsPool)
            {
                item.Value.DestroyPool();
            }
            _effectsPool.Clear();
            _effectsPool = null;
        }
        
        private void RegisterJob<T>() where T : BuffBase
        {
            _effectsPool.Add(typeof(T),new ObjectPool<BuffBase>(null,null, Activator.CreateInstance<T>));
        }
        
        public T CreateNewJob<T>() where T : BuffBase
        {
            Type type = typeof(T);
            if (!_effectsPool.ContainsKey(type))
            {
                throw new Exception($"unknown job type {type}");
            }
            
            return _effectsPool[type].Get() as T;
        }

        public void RecycleJob<T>(T job) where T : BuffBase
        {
            Type type = typeof(T);
            if (!_effectsPool.ContainsKey(type))
            {
                return;
            }
            
            job.Dispose();
            _effectsPool[type].Release(job);
        }

        public void RecycleJobList(ref List<BuffBase> jobList)
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
