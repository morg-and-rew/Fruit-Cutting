using System;
using System.Collections.Generic;
using UnityEngine;

namespace FruitCutting.Tools.Pool
{
    public sealed class ObjectsPool<T> where T : class
    {
        private Queue<T> _pool = new Queue<T>();
        private List<T> _activePool = new List<T>();
        private Func<T> _createObj;
        private Action<T> _addObj;
        private Action<T> _getObj;

        public int Amount => _pool.Count;

        public ObjectsPool(Func<T> createObj, Action<T> addObj, Action<T> getObj, uint initAmount)
        {
            if (createObj == null || addObj == null || getObj == null)
                return;

            _createObj = createObj;
            _addObj = addObj;
            _getObj = getObj;

            for (int i = 0; i < initAmount; i++)
            {
                Get();
            }
        }

        public T Get()
        {
            T item = _pool.Count > 0 ? _pool.Dequeue() : _createObj();
            _getObj(item);
            _activePool.Add(item);
            return item;
        }

        public void Add(T item)
        {
            _pool.Enqueue(item);
            _addObj(item);
            _activePool.Remove(item);
        }

        public void GetAll()
        {
            foreach (T item in _pool.ToArray())
            {
                _getObj(item);
                _activePool.Add(item);
            }

            _pool.Clear();
        }

        public void AddAll()
        {
            foreach (T item in _activePool.ToArray())
            {
                Add(item);
            }
        }
    }
}