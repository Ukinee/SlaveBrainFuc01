using Codebase.Forms.Views.Implementations;
using Codebase.Gameplay.Interface.Presentation.Interfaces;
using Codebase.Gameplay.Interface.Views.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Gameplay.Interface.Views.Implementations
{
    public class WinFormView : FormViewBase<IWinFormPresenter>, IWinFormView
    {
        [SerializeField] private Button _continueButton;

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(Continue);
        }

        protected override void OnBeforeDisable()
        {
            _continueButton.onClick.RemoveListener(Continue);
        }

        private void OnDestroy()
        {
            Presenter.OnViewDisposed();
        }

        private void Continue()
        {
            Presenter.OnContinueClick();
        }
    }
}
