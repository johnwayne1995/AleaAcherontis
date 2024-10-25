using System;
using UnityEngine;
namespace Modules
{
    public class Timer
    {
        private float _duration;
        private Action _onComplete;
        private Action<float> _onPerFrame;
        private Action<int> _onPerSecond;
        private Action<int> _onCountDown;
        private bool _isLoop;
        private bool _usesRealTime;
        private bool _hasAutoDestroyOwner;
        private GameObject _autoDestroyOwner;
        private float _startTime = 0f;
        private float _elapsedSecondTemp = 0f;
        private int _elapsedSecond = 0;
        private float _countDownDelta = 0f;
        private int _countTotal = 0;
        private int _elapsedDelta = 0;
        private float _elapsedDeltaTemp = 0f;

        private int _frame;
        private int _startFrame = 0;
        private bool _useFactor = true;

        public long useId { get; set; }
        public bool isCancelled { get; set; }

        private float GetWorldTime()
        {
            return _usesRealTime ? Time.realtimeSinceStartup : Time.time;
        }

        private float GetFireTime()
        {
            return _startTime + _duration;
        }

        private int GetCurrentFrame()
        {
            return Time.frameCount;
        }

        private int GetFireFrame()
        {
            return _startFrame + _frame;
        }

        public Timer()
        {
            Reset();
        }

        public void Recover(float duration = 0, Action onComplete = null, Action<float> onPerFrame = null, Action<int> onPerSecond = null,
            Action<int> onCountDown = null, bool isLoop = false, bool useRealTime = false, GameObject autoDestroyOwner = null, int frame = 0, 
            float countDownDelta = 0, int countTotal = 0, bool useFactor = true)
        {
            _duration = duration;
            _onComplete = onComplete;
            _onPerFrame = onPerFrame;
            _onPerSecond = onPerSecond;
            _onCountDown = onCountDown;
            _isLoop = isLoop;
            _usesRealTime = useRealTime;
            _hasAutoDestroyOwner = autoDestroyOwner != null;
            _autoDestroyOwner = autoDestroyOwner;
            _startTime = GetWorldTime();
            _startFrame = GetCurrentFrame();
            _frame = frame;
            _countDownDelta = countDownDelta;
            _countTotal = countTotal;
            _useFactor = useFactor;
            _elapsedSecondTemp = 0f;
            _elapsedSecond = 0;
            _elapsedDeltaTemp = 0f;
            _elapsedDelta = 0;
        }

        public void Reset()
        {
            this.useId = 0;
            _duration = 0f;
            _onComplete = null;
            _onPerFrame = null;
            _onPerSecond = null;
            _onCountDown = null;
            _isLoop = false;
            _usesRealTime = false;
            _startTime = 0;
            _startFrame = 0;
            _frame = 0;
            _countDownDelta = 0;
            _countTotal = 0;
            _elapsedSecondTemp = 0f;
            _elapsedSecond = 0;
            _elapsedDeltaTemp = 0f;
            _elapsedDelta = 0;
            _useFactor = true;

            this.isCancelled = false;
        }

        public void OnUpdate(float deltaTime, int factor)
        {
            if (this.isCancelled) return;
            if (deltaTime <= 0) return;
            if (_useFactor && factor == 0) return;
            if (_hasAutoDestroyOwner && _autoDestroyOwner == null)
            {
                this.isCancelled = true;
                return;
            }

            if (_onPerFrame != null) _onPerFrame(deltaTime);

            _elapsedSecondTemp += deltaTime;
            if (_elapsedSecondTemp >= 1f)
            {
                _elapsedSecondTemp -= 1;
                _elapsedSecond++;
                if (_onPerSecond != null) _onPerSecond(_elapsedSecond);
            }

            // 计次与循环不兼容, 先处理
            _elapsedDeltaTemp += deltaTime;
            if (_countTotal > 0 && _countDownDelta > 0 && _elapsedDeltaTemp >= _countDownDelta)
            {
                _elapsedDeltaTemp -= _countDownDelta;
                _elapsedDelta++;
                if (_onCountDown != null) _onCountDown(_elapsedDelta);
                if (_elapsedDelta == _countTotal)
                {
                    if (_onComplete != null) _onComplete();
                    this.isCancelled = true;
                    return;
                }
            }

            var worldTime = GetWorldTime();
            var fireTime = GetFireTime();
            // 暂不支持==0, 会影响帧计时器
            if (_duration > 0 && worldTime >= fireTime)
            {
                if (_onComplete != null) _onComplete();

                if (_isLoop)
                {
                    _startTime = worldTime;
                }
                else
                {
                    this.isCancelled = true;
                    return;
                }
            }

            var curFrame = GetCurrentFrame();
            var fireFrame = GetFireFrame();
            if (_frame > 0 && curFrame >= fireFrame)
            {
                if (_onComplete != null) _onComplete();
                this.isCancelled = true;
                return;
            }
        }
    }
}
