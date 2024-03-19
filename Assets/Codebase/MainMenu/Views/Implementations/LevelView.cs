using System;
using Codebase.Core.Frameworks.MVP.BaseClasses;
using Codebase.MainMenu.Presentations.Interfaces;
using Codebase.MainMenu.Views.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.MainMenu.Views.Implementations
{
    public class LevelView : ViewBase<ILevelPresenter>, ILevelView
    {
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _buyButton;
        [SerializeField] private TMP_Text _levelNameText;
        [SerializeField] private TMP_Text _priceText;

        private void OnEnable()
        {
            _selectButton.onClick.AddListener(OnSelectButtonClicked);
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
        }

        private void OnDisable()
        {
            _selectButton.onClick.RemoveListener(OnSelectButtonClicked);
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
        }

        private void OnDestroy() =>
            Presenter.OnViewDispose();

        public void SetPassed(bool isPassed)
        {
            //$"Is passed: {isPassed}".Log();
        }

        public void SetSelected(bool isSelected)
        {
            //$"Is selected: {isSelected}".Log();
        }

        public void UnParent() =>
            transform.SetParent(null);

        public void SetLevelName(string levelName) =>
            _levelNameText.text = levelName;

        public void SetPrice(int price) =>
            _priceText.text = price.ToString();

        public void SetUnlocked(bool isUnlocked)
        {
            _selectButton.gameObject.SetActive(isUnlocked);
            _buyButton.gameObject.SetActive(isUnlocked == false);
        }

        private void OnBuyButtonClicked() =>
            Presenter.OnBuyButtonClick();

        private void OnSelectButtonClicked() =>
            Presenter.OnSelectButtonClick();
    }
}
