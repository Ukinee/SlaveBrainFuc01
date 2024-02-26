using UnityEngine;

namespace Codebase.Core.Services.Common
{
    public interface IRaycastHitProvider
    {
        public bool HasHit { get; }
        public Vector3 HitPoint { get; }
        public RaycastHit Hit { get; }
        
        void OnUpdate(Vector3 mousePosition);
    }
}
