using Codebase.Balls.Services.Interfaces;
using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using UnityEngine;

namespace Codebase.Balls.Services.Implementations
{
    public class AimService : IAimService
    {
        private bool _isAiming;
        
        public Vector3 AimPosition { get; private set; }

        public void StartAim()
        {
            _isAiming = true;
        }

        public void SetPosition(Vector3 position)
        {
            AimPosition = position;
            AimPosition.Log(); 
        }

        public void EndAim()
        {
            _isAiming = false;
        }
    }
}
