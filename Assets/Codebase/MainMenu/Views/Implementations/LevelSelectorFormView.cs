using System;
using Codebase.Forms.Views.Implementations;
using Codebase.MainMenu.Presentations.Interfaces;
using Codebase.MainMenu.Views.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.MainMenu.Views.Implementations
{
    public class LevelSelectorFormView : FormViewBase<ILevelSelectingFormPresenter>, ILevelSelectingFormView
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _startButton;
        
        [SerializeField] private GameObject _levelContents;
        [SerializeField] private GameObject _mapContents;
        
        private void OnEnable()
        {
            _backButton.onClick.AddListener(OnBackClicked);
            _startButton.onClick.AddListener(OnStartClicked);
            Presenter?.Enable();
        }

        protected override void OnBeforeDisable()
        {
            _backButton.onClick.RemoveListener(OnBackClicked);
            _startButton.onClick.RemoveListener(OnStartClicked);
        }

        private void OnDestroy()
        {
            Presenter.OnViewDispose();
        }

        public void SetChild(ILevelSelectorPartView view)
        {
        }

        private void OnStartClicked()
        {
            Presenter.OnStartClicked();
        }

        private void OnBackClicked()
        {
            Presenter.OnBackClicked();
        }

        public void AddLevel(ILevelView view)
        {
            ((MonoBehaviour)view).transform.SetParent(_levelContents.transform);
        }

        public void AddMap(IMainMenuMapView view)
        {
            ((MonoBehaviour)view).transform.SetParent(_mapContents.transform);
        }
    }
}
