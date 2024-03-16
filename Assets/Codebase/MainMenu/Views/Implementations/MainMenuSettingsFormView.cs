using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Forms.Views.Implementations;
using Codebase.Forms.Views.Interfaces;
using Codebase.MainMenu.Presentations.Interfaces;
using Codebase.MainMenu.Views.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.MainMenu.Views.Implementations
{
    public class MainMenuSettingsFormView : FormViewBase<IMainMenuSettingsFormPresenter>, IMainMenuSettingsView
    {
        [SerializeField] private Button _soundMuteButton;
        [SerializeField] private Button _musicMuteButton;
        [SerializeField] private Button _backButton;

        private void OnEnable()
        {
            _soundMuteButton.onClick.AddListener(OnSoundMuteButtonClicked);
            _musicMuteButton.onClick.AddListener(OnMusicMuteButtonClicked);
            _backButton.onClick.AddListener(OnBackButtonClicked);
        }

        protected override void OnBeforeDisable()
        {
            _soundMuteButton.onClick.RemoveListener(OnSoundMuteButtonClicked);
            _musicMuteButton.onClick.RemoveListener(OnMusicMuteButtonClicked);
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
        }

        private void OnDestroy() =>
            Presenter.OnViewDisposed();

        private void OnBackButtonClicked() =>
            Presenter.OnClickBack();

        private void OnMusicMuteButtonClicked() =>
            Presenter.OnClickMusic();

        private void OnSoundMuteButtonClicked() =>
            Presenter.OnClickSound();

        public void SetMusicMuteButtonState(bool isMuted)
        {
            if (isMuted)
                "Setting music to mute".Log();
            else
                "Setting music to unmute".Log();
        }

        public void SetSoundMuteButtonState(bool isMuted)
        {
            if (isMuted)
                "Setting sound to mute".Log();
            else
                "Setting sound to unmute".Log();
        }
    }
}
