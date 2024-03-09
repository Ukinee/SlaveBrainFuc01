using Codebase.Core.Services.Common;
using Codebase.Forms.Common.FormTypes.MainMenu;
using Codebase.Forms.Presentations.Interfaces;
using Codebase.Forms.Presentations.Interfaces.MainMenu;
using Codebase.Forms.Services.Implementations;
using Codebase.Forms.Views.Interfaces;

namespace Codebase.Forms.Presentations.Implementations.MainMenu
{
    public class MainMenuSettingsFormPresenter : IMainMenuSettingsFormPresenter
    {
        private readonly IInterfaceService _interfaceService;
        private readonly IAudioService _audioService;
        private readonly IMainMenuSettingsView _view;

        public MainMenuSettingsFormPresenter
            (IInterfaceService interfaceService, IAudioService audioService, IMainMenuSettingsView view)
        {
            _interfaceService = interfaceService;
            _audioService = audioService;
            _view = view;
        }

        public void Enable()
        {
            _view.SetMusicMuteButtonState(_audioService.IsMusicMuted);
            _view.SetSoundMuteButtonState(_audioService.IsSoundMuted);
        }

        public void Disable()
        {
        }

        public void OnClickBack()
        {
            _interfaceService.Hide(new MainMenuSettingsFormType());
        }

        public void OnClickSound()
        {
            if (_audioService.IsSoundMuted)
                _audioService.UnmuteSound();
            else
                _audioService.MuteSound();
            
            _view.SetSoundMuteButtonState(_audioService.IsSoundMuted);
        }
        
        public void OnClickMusic()
        {
            if (_audioService.IsMusicMuted)
                _audioService.UnmuteMusic();
            else
                _audioService.MuteMusic();
            
            _view.SetMusicMuteButtonState(_audioService.IsMusicMuted);
        }
    }
}
