using System;
using UnityEngine;

namespace Codebase.Core.Services.AudioService.Implementation.AudioEntries.Base
{
    [Serializable]
    public abstract class AudioEntry
    {
        [field: SerializeField] public AudioClip Clip { get; private set; }
        [field: SerializeField] public float Volume { get; private set; }
    }
}
