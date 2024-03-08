using Codebase.Core.Frameworks.MVP.BaseClasses;
using Codebase.Core.Frameworks.MVP.Interfaces;
using Codebase.Forms.Views.Interfaces;
using UnityEngine;

namespace Codebase.Forms.Views.Implementations
{
    public class FormViewBase<T> : ViewBase<T>, IFormView where T : IPresenter
    {
        [SerializeField] private GameObject _content;

        private void OnDisable()
        {
            OnBeforeDisable();
            Presenter.Disable();
        }

        public void SetVisibility(bool isVisible) =>
            _content.SetActive(isVisible);

        protected virtual void OnBeforeDisable()
        {
        }
    }
}
