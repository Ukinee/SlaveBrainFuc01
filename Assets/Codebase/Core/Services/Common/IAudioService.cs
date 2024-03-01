using System;
using Codebase.Core.Services.AudioService.Common;

namespace Codebase.Core.Services.Common
{
    public interface IAudioService
    {
        public void PlaySound<T>(T sound) where T : Enum;
        public void PlayMusic(MusicSounds music);
        
        public void Pause();
        public void Resume();
    }
}
