using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
namespace Modules
{
    public class TimerModule : AbstractModule<TimerModule>
    {
        private List<Timer> _timers;
        private ObjectPool<Timer> _objectPool;
        private long _allocUseId = 1;
        private int _scalaFactor = 1;

        public long AllocUseId
        {
            get { return ++_allocUseId; }
        }

        public override bool NeedUpdate => true;

        public long Register(float duration = 0, Action onComplete = null, Action<float> onPerFrame = null, Action<int> onPerSecond = null,
            Action<int> onCountDown = null, bool isLoop = false, bool useRealTime = false, GameObject autoDestroyOwner = null, int frame = 0,
            float countDownDelta = 0, int countTotal = 0, bool userFactor = true)
        {
            var timer = this.Get();
            timer.useId = AllocUseId;
            timer.Recover(duration, onComplete, onPerFrame, onPerSecond, onCountDown, isLoop, useRealTime, autoDestroyOwner, frame, countDownDelta, countTotal, userFactor);
            return timer.useId;
        }

        public void Cancel(long useId)
        {
            if (useId <= 0)
                return;

            //Timer timer = null;
            //for (var i = _timers.Count - 1; i >= 0; i--)
            //{
            //    timer = _timers[i];
            //    if (timer.useId == useId)
            //    {
            //        timer.Reset();
            //        _timers.RemoveAt(i);
            //        _objectPool.Release(timer);
            //        return;
            //    }
            //}
            Timer timer = null;
            if (TryGet(useId, ref timer))
            {
                timer.isCancelled = true;
            }
        }

        public void CancelAll()
        {
            for (var i = _timers.Count - 1; i >= 0; i--)
            {
                var timer = _timers[i];
                _objectPool.Release(timer);    
            }
            _timers.Clear();
            _allocUseId = 1;
        }

        private Timer Get()
        {
            var timer = _objectPool.Get();
            _timers.Add(timer);

            return timer;
        }

        private bool TryGet(long useId, ref Timer timer)
        {
            for (var i = _timers.Count - 1; i >= 0; i--)
            {
                if (_timers[i].useId != useId)
                    continue;

                timer = _timers[i];
                return true;
            }

            return false;
        }

        private void RemoveTimer()
        {
            for (var i = _timers.Count - 1; i >= 0; i--)
            {
                var timer = _timers[i];
                if (timer.isCancelled)
                {
                    timer.Reset();
                    _timers.RemoveAt(i);
                    _objectPool.Release(timer);
                }
            }
        }

        private void OnGet(Timer timer)
        {
            timer.Reset();
        }

        private void OnRelease(Timer timer)
        {

        }

        public override void Start()
        {
            _timers = new List<Timer>();
            _objectPool = new ObjectPool<Timer>(OnGet, OnRelease);
        }

        public override void Dispose()
        {
            CancelAll();
        }

        public override void Update()
        {
            base.Update();
            RemoveTimer();
            for (var i = _timers.Count - 1; i >= 0; i--)
            {
                _timers[i].OnUpdate(Time.deltaTime, this._scalaFactor);
            }
            RemoveTimer();
        }

        public override void Pause()
        {
            this._scalaFactor = 0;
        }

        public override void Continue()
        {
            this._scalaFactor = 1;
        }
    }

    public class STimer
    {
        // 倒计时(单位:秒)
        public static long CountDown(int count, Action onComplete, Action<int> onCountDown, bool stopOnGameStop = true)
        {
            return CountDown(1, count, onComplete, onCountDown, stopOnGameStop);
        }

        public static long CountDown(float delta, int count, Action onComplete, Action<int> onCountDown, bool stopOnGameStop = true)
        {
            return Register(delta * count, 
                onComplete,
                null, 
                null,
                onCountDown, 
                false, 
                false, 
                null, 
                0, 
                delta, 
                count,
                stopOnGameStop
                );
        }

        public static long Wait(float duration, Action onComplete, bool stopOnGameStop = true)
        {
            return Register(duration, 
                onComplete,
                null,
                null,
                null,
                false,
                false,
                null,
                0,
                0,
                0,
                stopOnGameStop);
        }

        internal static long Run(Action<float> onPerFrame, bool stopOnGameStop = true)
        {
            return Register(-1, 
                null, 
                onPerFrame,
                null,
                null,
                true,
                false,
                null,
                0,
                0,
                0,
                stopOnGameStop
                );
        }

        internal static long RunByFrame(Action<float> onPerFrame, GameObject autoDestroyOwner, bool stopOnGameStop = true)
        {
            return Register(-1,
                null,
                onPerFrame,
                null,
                null,
                false,
                false,
                autoDestroyOwner,
                0,
                0,
                0,
                stopOnGameStop
                );
        }

        public static long RunBySecond(Action<int> onPerSecond, GameObject autoDestroyOwner, bool stopOnGameStop = true)
        {
            return Register(-1,
                null,
                null,
                onPerSecond,
                null,
                false,
                false,
                autoDestroyOwner,
                0,
                0,
                0,
                stopOnGameStop
                );
        }

        public static long DelayFrame(int frame, Action onComplete, bool stopOnGameStop = true)
        {
            return Register(0,
                 onComplete,
                 null,
                 null,
                 null,
                 false,
                 false,
                 null, 
                 frame,
                 0,
                 0,
                 stopOnGameStop
                 );
        }

        public static long Loop(float duration, Action onComplete, bool stopOnGameStop = true)
        {
            return Register(duration,
                 onComplete,
                 null,
                 null,
                 null,
                 true,
                 false,
                 null,
                 0,
                 0,
                 0,
                 stopOnGameStop
                 );
        }

        internal static long Register(float duration = 0, Action onComplete = null, Action<float> onPerFrame = null, Action<int> onPerSecond = null,
            Action<int> onCountDown = null, bool isLoop = false, bool useRealTime = false, GameObject autoDestroyOwner = null, int frame = 0,
            float countDownDelta = 0, int countTotal = 0, bool useFactor = true)
        {
            return TimerModule.Instance.Register(duration, onComplete, onPerFrame, onPerSecond, onCountDown, isLoop, useRealTime, autoDestroyOwner, 
                frame, countDownDelta, countTotal, useFactor);
        }

        public static void Cancel(long useId)
        {
            TimerModule.Instance.Cancel(useId);        
        }
    }
}
