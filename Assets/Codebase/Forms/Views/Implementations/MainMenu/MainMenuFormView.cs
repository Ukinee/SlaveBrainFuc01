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
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _leaderboardButton;
        [SerializeField] private Button _shopButton;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartButtonClicked);
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            _leaderboardButton.onClick.AddListener(OnLeaderboardButtonClicked);
            _shopButton.onClick.AddListener(OnShopButtonClicked);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClicked);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            _leaderboardButton.onClick.RemoveListener(OnLeaderboardButtonClicked);
            _shopButton.onClick.RemoveListener(OnShopButtonClicked);
        }

        public void Set(string value) =>
            _coinAmount.text = value;

        private void OnStartButtonClicked() =>
            Presenter.OnClickStart();

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
