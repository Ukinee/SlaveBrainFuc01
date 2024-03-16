using System;
using Codebase.Forms.Presentations.Interfaces.MainMenu;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Forms.Views.Implementations.MainMenu
{
    public class MainMenuShopFormView : FormViewBase<IMainMenuShopFormPresenter>
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
            Presenter.OnClickBack();
        }
    }
}
