using UnityEngine;

namespace Codebase.Balls.Views.Interfaces
{
    public interface IAimView
    {
        public void SetPoints(Vector3 startPoint, Vector3 endPoint);
        void Enable();
        void Disable();
    }
}
