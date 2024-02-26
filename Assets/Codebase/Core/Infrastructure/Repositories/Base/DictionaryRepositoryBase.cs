using System.Collections.Generic;

namespace ApplicationCode.Core.Infrastructure.Repositories.Base
{
    public abstract class DictionaryRepositoryBase<TKey, TValue> : IReadonlyDictionaryRepository<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();

        public IEnumerable<TKey> Keys => _dictionary.Keys;

        public void Register(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
            
            OnAdd(key, value);
        }

        public TValue Get(TKey key) =>
            _dictionary[key];

        public void Remove(TKey key) =>
            _dictionary.Remove(key);

        public bool Contains(TKey key) =>
            _dictionary.ContainsKey(key);

        protected virtual void OnAdd(TKey key, TValue value)
        {
        }
    }
}