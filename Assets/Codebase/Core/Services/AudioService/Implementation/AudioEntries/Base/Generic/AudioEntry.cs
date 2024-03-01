using System;
using UnityEngine;

namespace Codebase.Core.Services.AudioService.Implementation.AudioEntries.Base.Generic
{
    [Serializable]
    public abstract class AudioEntry<T> : AudioEntry where T : Enum
    {
        [field: SerializeField] public T Type { get; private set; }
    }
}
