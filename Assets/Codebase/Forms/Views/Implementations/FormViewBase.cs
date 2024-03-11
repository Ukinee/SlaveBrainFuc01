using Codebase.Core.Frameworks.MVP.BaseClasses;
using Codebase.Core.Frameworks.MVP.Interfaces;
using Codebase.Forms.Views.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Codebase.Forms.Views.Implementations
{
    public class FormViewBase<T> : ViewBase<T>, IFormView where T : IPresenter
    {
        [field: SerializeField] protected GameObject Content { get; private set; }

        private void OnDisable()
        {
            OnBeforeDisable();
            Presenter.Disable();
        }

        public void SetVisibility(bool isVisible) =>
            Content.SetActive(isVisible);

        protected virtual void OnBeforeDisable()
        {
        }
    }
}
