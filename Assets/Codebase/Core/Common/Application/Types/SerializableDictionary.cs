using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Codebase.Core.Common.Application.Types
{
    public class SerializableDictionary<TKey, TValue>
    {
        public List<Entry<TKey, TValue>> List;

        public Dictionary<TKey, TValue> Dictionary => List.ToDictionary(e => e.Key, e => e.Value);
    }

    [Serializable]
    public class Entry<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;
    }
}
