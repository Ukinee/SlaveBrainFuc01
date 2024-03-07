using Codebase.Balls.Services.Interfaces;
using Codebase.Balls.Views.Interfaces;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Common.General.Extensions.UnityVector3Extensions;
using Codebase.Core.Common.General.Utils;
using Codebase.Tanks.CQRS;
using UnityEngine;

namespace Codebase.Balls.Services.Implementations
{
    public class AimService : IAimService
    {
        private readonly GetTankPositionQuery _getTankPositionQuery;
        private readonly IAimView _aimView;
        private bool _isAiming;
        
        private static readonly int SWallLayerMask = 1 << LayerMask.NameToLayer(GameConstants.Wall);
        private static readonly int SBlockLayerMask = 1 << LayerMask.NameToLayer(GameConstants.Block);
        private static readonly int SLayerMask = SWallLayerMask | SBlockLayerMask;


        public AimService(GetTankPositionQuery getTankPositionQuery, IAimView aimView)
        {
            _getTankPositionQuery = getTankPositionQuery;
            _aimView = aimView;
            _aimView.Disable();
        }

        public Vector3 AimPosition { get; private set; }

        public void StartAim(Vector3 position)
        {
            _isAiming = true;
            _aimView.Enable();
            SetPosition(position);
        }

        public void SetPosition(Vector3 position)
        {
            AimPosition = position.WithY(GameConstants.YOffset);
            
            if(_isAiming == false)
                return;

            Vector3 tankPosition = _getTankPositionQuery.Execute().WithY(GameConstants.YOffset);
            Vector3 direction = AimPosition - tankPosition;
            direction = Vector3.ProjectOnPlane(direction, Vector3.up).normalized;

            if (Physics.Raycast(tankPosition, direction, out RaycastHit hit, float.MaxValue, SLayerMask) == false)
                MaloyAlert.Error($"Raycast error in {nameof(AimService)}");

            AimPosition = hit.point;
            _aimView.SetPoints(tankPosition, AimPosition);
        }

        public void EndAim(Vector3 position)
        {
            SetPosition(position);
            
            _isAiming = false;
            _aimView.Disable();
        }
    }
}
