using System;
using System.Collections.Generic;
using Codebase.Core.Services.AudioService.Common;
using Codebase.Core.Services.AudioService.Implementation.AudioEntries.Base;
using Codebase.Core.Services.AudioService.Implementation.AudioEntries.Base.Generic;
using Codebase.Core.Services.Common;
using UnityEngine;

namespace Codebase.Core.Services.AudioService.Implementation
{
    public class AudioService : IAudioService
    {
        private readonly Dictionary<Type, AudioEntry[]> _audioEntries;
        private readonly AudioSourcePoolService _audioSourcePoolService;
        private readonly AudioSource _backgroundMusicPlayer;

        public AudioService(AudioSourcePoolService audioSourcePoolService, Dictionary<Type, AudioEntry[]> audioEntries)
        {
            _audioSourcePoolService = audioSourcePoolService;
            _audioEntries = audioEntries;

            _backgroundMusicPlayer = _audioSourcePoolService.Get();
            _backgroundMusicPlayer.Stop();
            _backgroundMusicPlayer.loop = true;
        }

        public void PlaySound<T>(T sound) where T : Enum
        {
            AudioEntry<T> entry = Get(sound);

            _audioSourcePoolService.Get().PlayOneShot(entry.Clip, entry.Volume);
        }

        public void PlayMusic(MusicSounds music)
        {
            AudioEntry<MusicSounds> entry = Get(music);

            _backgroundMusicPlayer.clip = entry.Clip;
            _backgroundMusicPlayer.volume = entry.Volume;
            _backgroundMusicPlayer.Play();
        }
        
        public void Pause() =>
            _audioSourcePoolService.Pause();

        public void Resume() =>
            _audioSourcePoolService.Resume();

        private AudioEntry<T> Get<T>(T sound) where T : Enum
        {
            AudioEntry[] collection = _audioEntries[sound.GetType()];
            AudioEntry<T> foundEntry = null;

            foreach (AudioEntry audioEntry in collection)
            {
                if (audioEntry is not AudioEntry<T> typedEntry)
                    throw new Exception("AudioEntry is not of type AudioEntry<T>");

                if (EqualityComparer<T>.Default.Equals(typedEntry.Type, sound) == false)
                    continue;

                foundEntry = typedEntry;
            }

            if (foundEntry == null)
                throw new Exception("AudioEntry not found");

            return foundEntry;
        }
    }
}
