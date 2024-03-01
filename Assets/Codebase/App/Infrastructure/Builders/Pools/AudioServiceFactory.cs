using System;
using System.Collections.Generic;
using Codebase.Core.Services.AudioService.Implementation;
using Codebase.Core.Services.AudioService.Implementation.AudioEntries.Base;
using UnityEngine;

namespace Codebase.App.Infrastructure.Builders.Pools
{
    public class AudioServiceFactory
    {
        public AudioService Create()
        {
            AudioSourcePoolService audioSourcePoolService = CreateAudioSourcePoolService();
            
            Dictionary<Type, AudioEntry[]> audioEntryDictionary = new Dictionary<Type, AudioEntry[]>
            {
            };

            return new AudioService(audioSourcePoolService, audioEntryDictionary);
        }

        private static AudioSourcePoolService CreateAudioSourcePoolService()
        {
            AudioSource CreateAudioSource() =>
                new GameObject(nameof(AudioSource)).AddComponent<AudioSource>();

            AudioSourcePoolService audioSourcePoolService = new AudioSourcePoolService(CreateAudioSource);

            return audioSourcePoolService;
        }
    }
}
