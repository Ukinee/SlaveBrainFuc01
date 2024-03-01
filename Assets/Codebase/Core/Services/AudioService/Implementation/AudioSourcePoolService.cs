using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Codebase.Core.Services.AudioService.Implementation
{
    public class AudioSourcePoolService
    {
        private readonly List<AudioSource> _pool = new List<AudioSource>();
        private readonly Func<AudioSource> _factory;
        private readonly AudioSourcePoolTag _audioSourcePoolTag;

        public AudioSourcePoolService(Func<AudioSource> factory)
        {
            _factory = factory;

            _audioSourcePoolTag = Object.FindObjectOfType<AudioSourcePoolTag>()
                                  ?? new GameObject(nameof(AudioSourcePoolTag)).AddComponent<AudioSourcePoolTag>();
            
            Object.DontDestroyOnLoad(_audioSourcePoolTag);
        }

        public void Pause()
        { 
            foreach (AudioSource source in _pool)
                source.Pause();
        }
        
        public void Resume()
        {
            foreach (AudioSource source in _pool)
                source.UnPause();
        }
        
        public AudioSource Get()
        {
            AudioSource source = _pool.FirstOrDefault(x => x.isPlaying == false);

            return source == null ? Expand() : source;
        }

        private AudioSource Expand()
        {
            AudioSource source = _factory.Invoke();
            source.transform.SetParent(_audioSourcePoolTag.transform);
            _pool.Add(source);
            
            return source;
        }
    }
}
