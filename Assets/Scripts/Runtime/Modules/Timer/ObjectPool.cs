using System;
using System.Collections.Generic;
using UnityEngine;
namespace Modules
{
    internal class ObjectPool<T> where T : class
    {
        private readonly HashSet<T> _objInPool = new();
        private readonly Stack<T> _stack = new();
        private readonly Action<T> _actionOnGet;
        private readonly Action<T> _actionOnRelease;
        private readonly Func<T> _crtor;

        public ObjectPool(Action<T> actionOnGet, Action<T> actionOnRelease, Func<T> crtor = null)
        {
            _actionOnGet = actionOnGet;
            _actionOnRelease = actionOnRelease;
            _crtor = crtor;
        }

        private int _countAll;

        public int CountAll => _countAll;

        public int CountActive => CountAll - CountInactive;

        public int CountInactive => _stack.Count;

        public T Get()
        {
            T obj;
            if (_stack.Count == 0)
            {
                obj = _crtor != null ? _crtor() : Activator.CreateInstance<T>();
                ++_countAll;
            }
            else
            {
                obj = _stack.Pop();
                _objInPool.Remove(obj);
            }

            _actionOnGet?.Invoke(obj);
            return obj;
        }

        public void Release(T element)
        {
            _actionOnRelease?.Invoke(element);
            if (_objInPool.Add(element))
            {
                _stack.Push(element);
            }
            else
            {
                Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
            }
        }

        public void DestroyPool()
        {
            if (_stack.Count == 0) return;

            _stack.Clear();
        }
    }
}
