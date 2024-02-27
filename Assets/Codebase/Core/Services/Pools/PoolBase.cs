using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Codebase.Core.Services.Pools
{
    public abstract class PoolBase<T> : IPool<T> where T : MonoBehaviour
    {
        private GameObject _gameObject;
        
        private readonly Func<T> _factory;
        private readonly Queue<T> _pool = new Queue<T>();
        private readonly List<T> _wanderingObjects = new List<T>();

        protected PoolBase(Func<T> factory)
        {
            _gameObject = new GameObject($"Pool_{typeof(T).Name}");
            Object.DontDestroyOnLoad(_gameObject);
            
            _factory = factory;
        }

        public void Release(T obj)
        {
            OnBeforeReturn(obj);
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_gameObject.transform);
            _wanderingObjects.Remove(obj);
            _pool.Enqueue(obj);
        }

        public void ReleaseAll()
        {
            for (int i = _wanderingObjects.Count - 1; i >= 0; i--)
                Release(_wanderingObjects[i]);
        }

        protected T GetInternal()
        {
            if(_pool.Count == 0)
                Expand();
            
            T obj = _pool.Dequeue();
            _wanderingObjects.Add(obj);
            obj.gameObject.SetActive(true);
            
            return obj;
        }

        protected void Expand()
        {
            T obj = _factory.Invoke();
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_gameObject.transform);
            _pool.Enqueue(obj);
            
            OnCreate(obj);
        }

        protected virtual void OnBeforeReturn(T obj)
        {
        }

        protected virtual void OnCreate(T obj)
        {
        }
    }
}
