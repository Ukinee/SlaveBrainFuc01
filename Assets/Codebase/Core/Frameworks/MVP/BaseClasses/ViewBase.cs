using System;
using Codebase.Core.Frameworks.MVP.Interfaces;
using UnityEngine;

namespace Codebase.Core.Frameworks.MVP.BaseClasses
{
    public abstract class ViewBase<TPresenter> : MonoBehaviour, IView<TPresenter> where TPresenter : IPresenter
    {
        protected TPresenter Presenter { get; private set; }

        public void Construct(TPresenter presenter)
        {
            if (Presenter != null)
                throw new InvalidOperationException($"{GetType().Name} View already constructed");

            Presenter = presenter;
        }

        protected void ResetPresenter()
        {
            Presenter = default;
        }
    }
}
