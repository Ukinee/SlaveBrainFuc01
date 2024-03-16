using Codebase.Forms.Views.Implementations;
using Codebase.Gameplay.Interface.Presentation.Interfaces;
using Codebase.Gameplay.Interface.Views.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Gameplay.Interface.Views.Implementations
{
    public class WinFormView : FormViewBase<IWinFormPresenter>, IWinFormView
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private TMP_Text _coinAmount;

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

        public void SetCoinAmount(int amount)
        {
            _coinAmount.text = amount.ToString();
        }

        private void Continue()
        {
            Presenter.OnContinueClick();
        }
    }
}
