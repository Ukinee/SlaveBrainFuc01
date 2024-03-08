using System;
using Codebase.Core.Services.AudioService.Common;

namespace Codebase.Core.Services.Common
{
    public interface IAudioService
    {
        public bool IsMusicMuted { get; }
        public bool IsSoundMuted { get; }

        public void PlaySound<T>(T sound) where T : Enum;
        public void PlayMusic(MusicSounds music);

        public void MuteMusic();
        public void UnmuteMusic();

        public void MuteSound();
        public void UnmuteSound();
        
        public void Pause();
        public void Resume();
    }
}
