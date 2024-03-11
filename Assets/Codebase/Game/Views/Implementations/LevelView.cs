using System;
using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Core.Common.General.Utils;
using Codebase.Core.Frameworks.MVP.BaseClasses;
using Codebase.Game.Presentations.Interfaces;
using Codebase.Game.Views.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Game.Views.Implementations
{
    public class LevelView : ViewBase<ILevelPresenter>, ILevelView
    {
        [SerializeField] private Button _button;

        private void OnEnable()
        { 
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        { 
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        public void SetPassed(bool isPassed)
        {
            $"Is passed: {isPassed}".Log();
        }

        public void SetSelected(bool isSelected)
        {
            $"Is selected: {isSelected}".Log();
        }

        public void UnParent()
        {
            transform.SetParent(null);
        }

        private void OnButtonClicked()
        {
            Presenter.OnButtonClick();
        }
    }
}
