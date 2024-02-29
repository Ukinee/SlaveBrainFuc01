using Codebase.Balls.Services.Interfaces;
using Codebase.Tanks.CQRS;
using UnityEngine;

namespace Codebase.Balls.Services.Implementations
{
    public class AimService : IAimService
    {
        private readonly GetTankPositionQuery _getTankPositionQuery;
        private bool _isAiming;

        public AimService(GetTankPositionQuery getTankPositionQuery)
        {
            _getTankPositionQuery = getTankPositionQuery;
        }
        
        public Vector3 AimPosition { get; private set; }

        public void StartAim()
        {
            _isAiming = true;
        }

        public void SetPosition(Vector3 position)
        {
            AimPosition = position;
        }

        public void EndAim()
        {
            _isAiming = false;
        }
    }
}
