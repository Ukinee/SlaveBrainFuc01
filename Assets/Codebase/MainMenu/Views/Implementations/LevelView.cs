﻿using System;
using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Core.Frameworks.MVP.BaseClasses;
using Codebase.Game.Views.Interfaces;
using Codebase.MainMenu.Presentations.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.MainMenu.Views.Implementations
{
    public class LevelView : ViewBase<ILevelPresenter>, ILevelView
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _levelNameText;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnDestroy()
        {
            Presenter.OnViewDispose();
        }

        public void SetPassed(bool isPassed)
        {
            if (isPassed)
                $"Is passed: {isPassed}".Log();
        }

        public void SetSelected(bool isSelected)
        {
            //$"Is selected: {isSelected}".Log();
        }

        public void UnParent()
        {
            transform.SetParent(null);
        }

        public void SetLevelName(string levelName)
        {
            _levelNameText.text = levelName;
        }

        private void OnButtonClicked()
        {
            Presenter.OnButtonClick();
        }
    }
}
