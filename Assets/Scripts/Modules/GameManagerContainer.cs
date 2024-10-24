using System.Collections.Generic;
using Interfaces;
using Managers;

namespace Modules
{
    public class GameManagerContainer : AbstractModule<GameManagerContainer>
    {
        private List<IGameManager> _allGameManagers;
        private bool _registerFinish;
        
        public override void Start()
        {
            _allGameManagers = new List<IGameManager>();
            _registerFinish = false;

            RegisterGameManagers(new AudioManager());
            AwakeManagers();
        }

        public override void Update()
        {
            for (int i = 0, imax = _allGameManagers.Count; i < imax; i++)
            {
                _allGameManagers[i].Update();
            }
        }

        public override void LateUpdate()
        {
            for (int i = 0, imax = _allGameManagers.Count; i < imax; i++)
            {
                _allGameManagers[i].LateUpdate();
            }
        }

        public override void FixedUpdate()
        {
            for (int i = 0, imax = _allGameManagers.Count; i < imax; i++)
            {
                _allGameManagers[i].FixedUpdate();
            }
        }

        public override void Pause()
        {
            for (int i = 0, imax = _allGameManagers.Count; i < imax; i++)
            {
                _allGameManagers[i].PauseGame();
            }
        }

        public override void Continue()
        {
            for (int i = 0, imax = _allGameManagers.Count; i < imax; i++)
            {
                _allGameManagers[i].ContinueGame();
            }
        }
        
        public override void Dispose()
        {
        }
        
        private void RegisterGameManagers<T>(T manager) where T : class, IGameManager, new()
        {
            _allGameManagers.Add(manager);
            _registerFinish = true;
        }

        /// <summary>
        /// 获取管理器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetManager<T>() where T : class, IGameManager, new()
        {
            if (!_registerFinish)
                return null;
            
            int mgrId = MgrIdMap<T>.Id;
            return _allGameManagers[mgrId] as T;
        }
        
        private void AwakeManagers()
        {
            for (int i = 0, imax = _allGameManagers.Count; i < imax; i++)
            {
                _allGameManagers[i].Awake();
            }
        }
    }
}
