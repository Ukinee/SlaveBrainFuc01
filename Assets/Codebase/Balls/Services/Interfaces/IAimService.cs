using UnityEngine;

namespace Codebase.Balls.Services.Interfaces
{
    public interface IAimService
    {
        public Vector3 AimPosition { get; }
        public void SetPosition(Vector3 position);
        public void EndAim();
        public void StartAim();
    }
}
