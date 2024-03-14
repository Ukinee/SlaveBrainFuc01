using Codebase.Core.Frameworks.MVP.BaseClasses;
using Codebase.Tank.Presentation.Interfaces;
using Codebase.Tank.View.Interfaces;
using UnityEngine;

namespace Codebase.Tanks.View.Implementations
{
    public class TankView : ViewBase<ITankPositionPresenter>, ITankView
    {
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public void OnDisable()
        {
            Presenter?.Disable();
        }

        public void SetPosition(Vector3 position)
        {
            if (_transform == null)
                return;

            _transform.position = position;
        }
    }
}
