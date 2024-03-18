using System;
using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Core.Frameworks.MVP.BaseClasses;
using Codebase.MainMenu.Presentations.Interfaces;
using Codebase.MainMenu.Views.Interfaces;
using Codebase.Maps.Common;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Codebase.MainMenu.Views.Implementations
{
    public class MainMenuMapView : ViewBase<IMainMenuMapPresenter>, IMainMenuMapView
    {
        [SerializeField] private TMP_Text _text; //todo: debug
        [SerializeField] private TMP_Text _priceText; //todo: debug
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _buyButton;

        private void OnEnable()
        {
            _selectButton.onClick.AddListener(OnSelectButtonClick);
            _buyButton.onClick.AddListener(OnBuyButtonClick);
        }

        private void OnDisable()
        {
            _selectButton.onClick.RemoveListener(OnSelectButtonClick);
            _buyButton.onClick.RemoveListener(OnBuyButtonClick);
        }

        private void OnDestroy()
        {
            Presenter.OnViewDisposed();
        }

        public void SetBought(bool isAvailable)
        {
            _selectButton.interactable = isAvailable;
            _buyButton.gameObject.SetActive(isAvailable == false);

            $"SetBought {isAvailable}".Log();
        }

        public void SetPrice(int price) =>
            _priceText.text = price.ToString();

        public void SetMapType(MapType type) =>
            _text.text = type.ToString();

        private void OnBuyButtonClick() =>
            Presenter.OnBuyButtonClick();

        private void OnSelectButtonClick() =>
            Presenter.OnSelectButtonClick();

        public void SetSelection(bool value)
        {
        }

        public void UnParent()
        {
            transform.SetParent(null);
        }
    }
}
