using System;
using Codebase.Forms.Presentations.Interfaces.MainMenu;
using Codebase.PlayerData.Views.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Forms.Views.Implementations.MainMenu
{
    public class MainMenuFormView : FormViewBase<IMainMenuFormPresenter>, ITextView
    {
        [SerializeField] private TMP_Text _coinAmount;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _leaderboardButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _levelSelectionButton;

        private void OnEnable()
        {
            _shopButton.onClick.AddListener(OnShopButtonClicked);
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            _leaderboardButton.onClick.AddListener(OnLeaderboardButtonClicked);
            _levelSelectionButton.onClick.AddListener(OnLevelSelectionButtonClicked);
        }

        private void OnDisable()
        {
            _levelSelectionButton.onClick.RemoveListener(OnLevelSelectionButtonClicked);
            _leaderboardButton.onClick.RemoveListener(OnLeaderboardButtonClicked);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            _shopButton.onClick.RemoveListener(OnShopButtonClicked);
        }

        private void OnDestroy()
        {
            Presenter.OnViewDisposed();
        }

        public void Set(string value) =>
            _coinAmount.text = value;

        private void OnLevelSelectionButtonClicked() =>
            Presenter.OnClickLevelSelection();

        private void OnSettingsButtonClicked() =>
            Presenter.OnClickSettings();

        private void OnLeaderboardButtonClicked() =>
            Presenter.OnClickLeaderboard();

        private void OnShopButtonClicked() =>
            Presenter.OnClickShop();

        public void Dispose()
        {
        }
    }
}
