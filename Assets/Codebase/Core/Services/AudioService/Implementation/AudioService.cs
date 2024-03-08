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

            _backgroundMusicPlayer = _audioSourcePoolService.Take();
            _backgroundMusicPlayer.Stop();
            _backgroundMusicPlayer.loop = true;
            _backgroundMusicPlayer.gameObject.name = nameof(_backgroundMusicPlayer);
        }

        public bool IsMusicMuted { get; private set; }
        public bool IsSoundMuted { get; private set; }

        public void PlaySound<T>(T sound) where T : Enum
        {
            if (IsSoundMuted)
                return;

            AudioEntry<T> entry = Get(sound);

            _audioSourcePoolService.Get().PlayOneShot(entry.Clip, entry.Volume);
        }

        public void PlayMusic(MusicSounds music)
        {
            if (IsMusicMuted)
                return;

            AudioEntry<MusicSounds> entry = Get(music);

            _backgroundMusicPlayer.clip = entry.Clip;
            _backgroundMusicPlayer.volume = entry.Volume;
            _backgroundMusicPlayer.Play();
        }

        public void MuteMusic()
        {
            _backgroundMusicPlayer.Stop();
            IsMusicMuted = true;
        }

        public void UnmuteMusic()
        {
            _backgroundMusicPlayer.Play();
            IsMusicMuted = false;
        }

        public void MuteSound()
        {
            if (IsSoundMuted)
                return;

            IsSoundMuted = true;

            _audioSourcePoolService.Pause();
        }

        public void UnmuteSound()
        {
            if (IsSoundMuted == false)
                return;

            IsSoundMuted = false;

            _audioSourcePoolService.Resume();
        }

        public void Pause()
        {
            _backgroundMusicPlayer.Pause();
            _audioSourcePoolService.Pause();
        }

        public void Resume()
        {
            if (IsMusicMuted == false)
                _backgroundMusicPlayer.Play();

            if (IsSoundMuted == false)
                _audioSourcePoolService.Resume();
        }

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
