using System;
using System.Collections.Generic;

namespace Codebase.Core.Services.Pools
{
    public abstract class PoolBase<T> : IPool<T>
    {
        private readonly Func<T> _factory;
        private readonly Queue<T> _pool = new Queue<T>();
        private readonly List<T> _wanderingObjects = new List<T>();
        
        protected IReadOnlyList<T> WanderingObjects => _wanderingObjects;

        protected PoolBase(Func<T> factory)
        {
            _factory = factory;
        }

        public void Release(T obj)
        {
            OnBeforeReturn(obj);
            _wanderingObjects.Remove(obj);
            _pool.Enqueue(obj);
        }

        public abstract void ReleaseAll();

        protected T GetInternal()
        {
            if (_pool.Count == 0)
                Expand();

            T obj = _pool.Dequeue();
            _wanderingObjects.Add(obj);

            return obj;
        }

        protected void Expand()
        {
            T obj = _factory.Invoke();
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
