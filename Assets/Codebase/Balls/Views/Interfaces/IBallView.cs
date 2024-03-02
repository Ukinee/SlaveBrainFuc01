using Codebase.Balls.Presentations.Interfaces;
using Codebase.Core.Frameworks.MVP.Interfaces;
using UnityEngine;

namespace Codebase.Balls.Views.Interfaces
{
    public interface IBallView : IView<IBallPresenter>
    {
        public void SetPosition(Vector3 position);

        public void ReturnToPool();
    }
}
