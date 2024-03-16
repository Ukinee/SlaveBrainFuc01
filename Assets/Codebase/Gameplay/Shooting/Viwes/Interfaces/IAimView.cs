using UnityEngine;

namespace Codebase.Gameplay.Shooting.Viwes.Interfaces
{
    public interface IAimView
    {
        public void SetPoints(Vector3 startPoint, Vector3 endPoint);
        void Enable();
        void Disable();
    }
}
