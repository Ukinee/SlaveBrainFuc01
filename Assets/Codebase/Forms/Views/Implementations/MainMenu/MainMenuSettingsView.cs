using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Forms.Presentations.Interfaces.MainMenu;
using Codebase.Forms.Views.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Forms.Views.Implementations.MainMenu
{
    public class MainMenuSettingsView : FormViewBase<IMainMenuSettingsFormPresenter>, IMainMenuSettingsView
    {
        [SerializeField] private Button _soundMuteButton;
        [SerializeField] private Button _musicMuteButton;

        private void OnEnable()
        {
            _soundMuteButton.onClick.AddListener(OnSoundMuteButtonClicked);
            _musicMuteButton.onClick.AddListener(OnMusicMuteButtonClicked);
        }

        protected override void OnBeforeDisable()
        {
            _soundMuteButton.onClick.RemoveListener(OnSoundMuteButtonClicked);
            _musicMuteButton.onClick.RemoveListener(OnMusicMuteButtonClicked);
        }

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
