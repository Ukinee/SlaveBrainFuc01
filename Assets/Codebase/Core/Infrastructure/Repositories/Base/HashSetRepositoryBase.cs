using System;
using System.Collections.Generic;
using System.Linq;

namespace Codebase.Core.Infrastructure.Repositories.Base
{
    public abstract class HashSetRepositoryBase<T>
    {
        private readonly HashSet<T> _hashSet = new HashSet<T>();
        
        public int Count => _hashSet.Count;
        public T[] Items => _hashSet.ToArray();

        public void Register(T item)
        {
            if (_hashSet.Add(item) == false)
                throw new InvalidOperationException("Item already exists in hashset.");

            OnRegister(item);
        }

        public void Remove(T item)
        {
            if (_hashSet.Remove(item))
                OnRemove(item);
        }

        protected T GetInternal(T dummy)
        {
            if (_hashSet.TryGetValue(dummy, out T actualValue) == false)
                throw new InvalidOperationException("Cant get value from hashset.");

            return actualValue;
        }

        protected virtual void OnRemove(T item)
        {
        }

        protected virtual void OnRegister(T item)
        {
        }
    }
}