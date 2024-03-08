using Codebase.Core.Frameworks.MVP.Interfaces;
using UnityEngine;

namespace Codebase.Balls.Presentations.Interfaces
{
    public interface IBallPresenter : IPresenter
    {
        public Vector3 Direction { get;  }

        public void Collide(Vector3 normal);
        public void OnDeactivatorCollision(Vector3 position);
        void Dispose();
    }
}
