﻿using Codebase.Core.Services.Common;
using Codebase.Core.Services.PauseServices;
using Codebase.Forms.Common.FormTypes.Gameplay;
using Codebase.Forms.Services.Implementations;
using Codebase.Game.Services.Implementations;
using Codebase.Gameplay.Interface.Presentation.Interfaces;
using Codebase.Gameplay.Interface.Views.Interfaces;

namespace Codebase.Gameplay.Interface.Presentation.Implementations
{
    public class PauseFormPresenter : IPauseFormPresenter
     {
        private readonly PauseService _pauseService;
        private readonly GameService _gameService;
        private readonly IAudioService _audioService;
        private readonly IInterfaceService _interfaceService;
        private readonly IPauseFormView _view;

        public PauseFormPresenter
        (
            PauseService pauseService,
            GameService gameService,
            IAudioService audioService,
            IInterfaceService interfaceService,
            IPauseFormView view
        )
        {
            _pauseService = pauseService;
            _gameService = gameService;
            _audioService = audioService;
            _interfaceService = interfaceService;
            _view = view;
        }
        
        public void Enable()
        {
        }

        public void Disable()
        {
        }

        public void OnResumeButtonPressed()
        {
            _interfaceService.Hide(new GameplayPauseFormType());
            _pauseService.ResumeGame();
        }

        public void OnMusicButtonPressed()
        {
            if (_audioService.IsMusicMuted)
                _audioService.UnmuteMusic();
            else
                _audioService.MuteMusic();
            
            _view.SetMusicMuteButtonState(_audioService.IsMusicMuted);
        }

        public void OnSoundButtonPressed()
        {
            if (_audioService.IsSoundMuted)
                _audioService.UnmuteSound();
            else
                _audioService.MuteSound();
            
            _view.SetSoundMuteButtonState(_audioService.IsSoundMuted);
        }

        public void OnExitButtonPressed()
        {
            _pauseService.ResumeGame();
            _gameService.End();
        }
     }
}