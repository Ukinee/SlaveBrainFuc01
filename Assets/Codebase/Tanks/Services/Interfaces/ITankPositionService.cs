using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Tank.Services.Interfaces
{
    public interface ITankPositionService
    {
        public bool IsMoving { get; }
        public UniTask SetRandomPosition();
    }
}
