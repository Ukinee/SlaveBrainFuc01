using System.Collections.Generic;

namespace Codebase.Core.Infrastructure.Repositories.Base
{
    public interface IReadonlyDictionaryRepository<TKey, out TValue>
    {
        public IEnumerable<TKey> Keys { get; }

        public TValue Get(TKey key);
        public bool Contains(TKey key);
    }
}
