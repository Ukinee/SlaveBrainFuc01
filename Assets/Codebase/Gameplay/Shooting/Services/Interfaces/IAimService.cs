using UnityEngine;

namespace Codebase.Gameplay.Shooting.Services.Interfaces
{
    public interface IAimService
    {
        public bool IsAiming { get; }
        public Vector3 AimPosition { get; }
        public void SetPosition(Vector3 position);
        public void EndAim(Vector3 position);
        public void StartAim(Vector3 position);
    }
}
