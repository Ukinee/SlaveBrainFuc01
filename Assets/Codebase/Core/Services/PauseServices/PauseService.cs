using Codebase.Core.Services.Common;
using UnityEngine;

namespace Codebase.Core.Services.PauseServices
{
    public class PauseService
    {
        private readonly IAudioService _audioService;
        private float _rememberedTimeScale;

        public PauseService(IAudioService audioService)
        {
            _audioService = audioService;
        }
        
        public bool IsPaused { get; private set; } = false;

        public bool GetStatus() =>
            IsPaused;

        public void ApplicationPause()
        {
            _audioService.Pause();
            _rememberedTimeScale = Time.timeScale;
            Time.timeScale = 0;
            IsPaused = true;
        }

        public void ApplicationResume()
        {
            _audioService.Resume();
            IsPaused = false;
            Time.timeScale = _rememberedTimeScale;
        }

        public void GameplayPause()
        {
            throw new System.NotImplementedException();
        }

        public void GameplayResume()
        {
            throw new System.NotImplementedException();
        }
    }
}
