using Codebase.Core.Frameworks.MVP.Interfaces;
using Codebase.Tank.Presentation.Interfaces;
using UnityEngine;

namespace Codebase.Tank.View.Interfaces
{
    public interface ITankView : IView<ITankPositionPresenter>
    {
        public void SetPosition(Vector3 position);
    }
}
