using Codebase.Core.Frameworks.MVP.BaseClasses;
using Codebase.Tank.Presentation.Interfaces;
using Codebase.Tank.View.Interfaces;
using UnityEngine;

namespace Codebase.Tank.View.Implementations
{
    public class TankView : ViewBase<ITankPositionPresenter>, ITankView
    {
        private Transform _transform;
        
        private void Awake()
        {
            _transform = transform;
        }

        public void SetPosition(Vector3 position)
        {
            _transform.position = position;
        }
    }
}
