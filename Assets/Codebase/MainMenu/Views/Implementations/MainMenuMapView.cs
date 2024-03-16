using System;
using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Core.Frameworks.MVP.BaseClasses;
using Codebase.MainMenu.Presentations.Interfaces;
using Codebase.MainMenu.Views.Interfaces;
using Codebase.Maps.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.MainMenu.Views.Implementations
{
    public class MainMenuMapView : ViewBase<IMainMenuMapPresenter>, IMainMenuMapView
    {
        [SerializeField] private TMP_Text _text; //todo: debug
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }
        
        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            Presenter.OnViewDisposed();
        }

        private void OnButtonClick()
        {
            Presenter.OnButtonClick();
        }

        public void SetMapType(MapType type)
        {
            _text.text = type.ToString();
        }

        public void SetSelection(bool value)
        {
            $"setting view to {value} selection".Log();
        }

        public void UnParent()
        {
            transform.SetParent(null);
        }
    }
}
