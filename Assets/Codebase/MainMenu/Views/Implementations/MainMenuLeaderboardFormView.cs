using Codebase.Forms.Presentations.Interfaces;
using Codebase.Forms.Views.Implementations;
using Codebase.MainMenu.Presentations.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.MainMenu.Views.Implementations
{
    public class MainMenuLeaderboardFormView : FormViewBase<IMainMenuLeaderboardFormPresenter>
    {
        [SerializeField] private Button _backButton;

        private void OnEnable()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
        }

        protected override void OnBeforeDisable()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
        }

        private void OnDestroy()
        {
            Presenter.OnViewDisposed();
        }

        private void OnBackButtonClicked()
        {
            Presenter.OnBackClick();
        }
    }
}
