using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Forms.Views.Implementations;
using Codebase.Gameplay.Interface.Presentation.Interfaces;
using Codebase.Gameplay.Interface.Views.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Gameplay.Interface.Views.Implementations
{
    public class InterfaceFormView : FormViewBase<IInterfaceFormPresenter>, IInterfaceFormView
    {
        [SerializeField] private Button _pauseButton;

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(OnPauseClick);
        }

        protected override void OnBeforeDisable()
        {
            _pauseButton.onClick.RemoveListener(OnPauseClick);
        }

        private void OnPauseClick()
        {
            Presenter.OnPauseButtonPressed();
        }
    }
}
