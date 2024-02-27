using Codebase.Core.Frameworks.MVP.Interfaces;
using UnityEngine;

namespace Codebase.Balls.Presentations.Interfaces
{
    public interface IBallPresenter : IPresenter
    {
        public void Move(float deltaTime);
        public void Collide(Vector3 normal);
        public void ReturnToPool();
    }
}
