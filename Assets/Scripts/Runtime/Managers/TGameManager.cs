using System;
using System.Collections.Generic;
using Interfaces;
namespace Managers
{
    public abstract class TGameManager<T> : IGameManager where T : class, new()
    {
        public int Id { get { return MgrIdMap<T>.Id; } }
        
        public void Awake()
        {
            OnAwake();
        }

        public void EnterGame()
        {
            OnEnterGame();
        }
        
        public void EnterScene()
        {
            OnEnterScene();
        }

        public void ExitScene()
        {
            OnExitScene();
        }
        
        public void Update()
        {
            OnUpdate();
        }
        
        public void LateUpdate()
        {
            OnLateUpdate();
        }
        
        public void FixedUpdate()
        {
            OnFixedUpdate();
        }

        public void Destroy()
        {
            OnBeforeDestroy();
        }

        public void PauseGame()
        {
            OnPauseGame();
        }

        public void ContinueGame()
        {
            OnContinueGame();
        }

        protected virtual void OnAwake()
        {
        }
        
        /**
         * 第一次进入游戏调用
         */
        protected virtual void OnEnterGame()
        {
        }
        /*
         * 进入场景初始化
         */
        protected virtual void OnEnterScene()
        {
        }
        /**
         * 退出场景释放
         */
        protected virtual void OnExitScene()
        {
        }
        
        /**
         * 销毁之前
         */
        protected virtual void OnBeforeDestroy()
        {
            
        }
        
        /// <summary>
        /// 游戏暂停
        /// </summary>
        protected virtual void OnPauseGame()
        {
            
        }

        /// <summary>
        /// 继续游戏
        /// </summary>
        protected virtual void OnContinueGame()
        {
            
        }
        
        protected virtual void OnUpdate()
        {
            
        }
        
        protected virtual void OnLateUpdate()
        {
            
        }
        
        protected virtual void OnFixedUpdate()
        {
            
        }
    }
    
    internal static class MgrIdMap
    {
        private static Dictionary<Type, int> _idMap = new();

        public static int Register(Type t)
        {
            if (_idMap.TryGetValue(t, out int resId)) return resId;

            resId = _idMap.Count;
            _idMap[t] = resId;
            return resId;
        }
    }

    internal static class MgrIdMap<T>
    {
        private static bool _inited = false;
        private static int _id = -1;

        public static int Id
        {
            get
            {
                if (!_inited)
                {
                    _inited = true;
                    _id = MgrIdMap.Register(typeof(T));
                }

                return _id;
            }
        }
    }
}
