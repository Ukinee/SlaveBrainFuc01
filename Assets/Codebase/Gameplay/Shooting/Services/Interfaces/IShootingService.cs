using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Gameplay.Shooting.Services.Interfaces
{
    public interface IShootingService : IDisposable
    {
        public bool IsShooting { get; }
        public UniTask Shoot(Vector3 targetPosition);
    }
}
