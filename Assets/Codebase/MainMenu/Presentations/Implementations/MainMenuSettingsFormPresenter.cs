using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Core.Services.Common;
using Codebase.Forms.Common.FormTypes.MainMenu;
using Codebase.Forms.Services.Implementations;
using Codebase.Forms.Views.Interfaces;
using Codebase.MainMenu.Presentations.Interfaces;

namespace Codebase.MainMenu.Presentations.Implementations
{
    public class MainMenuSettingsFormPresenter : IMainMenuSettingsFormPresenter
    {
        private readonly int _id;
        private readonly IInterfaceService _interfaceService;
        private readonly IAudioService _audioService;
        private readonly IMainMenuSettingsView _view;
        private readonly DisposeCommand _disposeCommand;

        public MainMenuSettingsFormPresenter
        (
            int id,
            IInterfaceService interfaceService,
            IAudioService audioService,
            IMainMenuSettingsView view,
            DisposeCommand disposeCommand
        )
        {
            _id = id;
            _interfaceService = interfaceService;
            _audioService = audioService;
            _view = view;
            _disposeCommand = disposeCommand;
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

        public void OnViewDisposed()
        {
            Dispose();
        }


        private void Dispose()
        {
            _disposeCommand.Handle(_id);
        }
    }
}
