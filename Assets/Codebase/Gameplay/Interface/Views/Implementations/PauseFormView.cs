using System;
using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Forms.Views.Implementations;
using Codebase.Gameplay.Interface.Presentation.Interfaces;
using Codebase.Gameplay.Interface.Views.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Gameplay.Interface.Views.Implementations
{
    public class PauseFormView : FormViewBase<IPauseFormPresenter>, IPauseFormView
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _musicButton;
        [SerializeField] private Button _soundButton;
        [SerializeField] private Button _exitButton;

        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(OnResumeButtonPressed);
            _musicButton.onClick.AddListener(OnMusicButtonPressed);
            _soundButton.onClick.AddListener(OnSoundButtonPressed);
            _exitButton.onClick.AddListener(OnExitButtonPressed);
        }

        protected override void OnBeforeDisable()
        {
            _resumeButton.onClick.RemoveListener(OnResumeButtonPressed);
            _musicButton.onClick.RemoveListener(OnMusicButtonPressed);
            _soundButton.onClick.RemoveListener(OnSoundButtonPressed);
            _exitButton.onClick.RemoveListener(OnExitButtonPressed);
        }

        private void OnDestroy()
        {
            Presenter.OnViewDisposed();
        }

        public void SetMusicMuteButtonState(bool isMusicMuted)
        {
            if (isMusicMuted)
                "Music is muted".Log();
            else
                "Music is unmuted".Log();
        }

        public void SetSoundMuteButtonState(bool isSoundMuted)
        {
            if (isSoundMuted)
                "Sound is muted".Log();
            else
                "Sound is unmuted".Log();
        }

        private void OnExitButtonPressed() =>
            Presenter.OnExitButtonPressed();

        private void OnSoundButtonPressed() =>
            Presenter.OnSoundButtonPressed();

        private void OnMusicButtonPressed() =>
            Presenter.OnMusicButtonPressed();

        private void OnResumeButtonPressed() =>
            Presenter.OnResumeButtonPressed();
    }
}
