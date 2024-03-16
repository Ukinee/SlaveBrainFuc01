using System;
using Codebase.Forms.Views.Implementations;
using Codebase.Gameplay.Interface.Presentation.Interfaces;
using Codebase.Gameplay.Interface.Views.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Gameplay.Interface.Views.Implementations
{
    public class InterfaceFormView : FormViewBase<IInterfaceFormPresenter>, IInterfaceFormView
    {
        [SerializeField] private Button _pauseButton;
        
        [SerializeField] private Slider _upgradeProgress;
        [SerializeField] private TMP_Text _coinAmount;
        [SerializeField] private TMP_Text _boolsToShootAmount;

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(OnPauseClick);
        }

        protected override void OnBeforeDisable()
        {
            _pauseButton.onClick.RemoveListener(OnPauseClick);
        }

        private void OnDestroy()
        {
            Presenter.OnViewDisposed();
        }

        private void OnPauseClick()
        {
            Presenter.OnPauseButtonPressed();
        }

        public void SetCoinAmount(int currentValue, int difference)
        {
            _coinAmount.text = currentValue.ToString();
        }

        public void SetMaxUpgradePoints(int upgradeMaxValue)
        {
            _upgradeProgress.maxValue = upgradeMaxValue;
        }

        public void SetBallsToShoot(int amount)
        {
            _boolsToShootAmount.text = amount.ToString();
        }

        public void SetUpgradePoints(int upgradeValue)
        {
            _upgradeProgress.value = upgradeValue;
        }
    }
}
