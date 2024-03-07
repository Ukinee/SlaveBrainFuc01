using UnityEngine;

namespace Codebase.Balls.Services.Interfaces
{
    public interface IShootingService
    {
        public bool IsBusy { get; }
        public void Shoot(Vector3 targetPosition);
    }
}
