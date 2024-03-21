﻿using Codebase.Balls.Views.Interfaces;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Common.General.Extensions.UnityVector3Extensions;
using Codebase.Gameplay.Shooting.Services.Interfaces;
using Codebase.Gameplay.Shooting.Viwes.Interfaces;
using Codebase.Tanks.CQRS;
using UnityEngine;

namespace Codebase.Gameplay.Shooting.Services.Implementations
{
    public class AimService : IAimService
    {
        private readonly GetTankPositionQuery _getTankPositionQuery;
        private readonly IAimView _aimView;

        private static readonly int SWallLayerMask = 1 << LayerMask.NameToLayer(UnityConstants.Wall);
        private static readonly int SBlockLayerMask = 1 << LayerMask.NameToLayer(UnityConstants.Block);
        private static readonly int SLayerMask = SWallLayerMask | SBlockLayerMask;

        public AimService(GetTankPositionQuery getTankPositionQuery, IAimView aimView)
        {
            _getTankPositionQuery = getTankPositionQuery;
            _aimView = aimView;
            _aimView.Disable();
        }

        public bool IsAiming { get; private set; }
        public Vector3 AimPosition { get; private set; }

        public void StartAim(Vector3 position)
        {
            IsAiming = true;
            _aimView.Enable();
            SetPosition(position);
        }

        public void SetPosition(Vector3 position)
        {
            AimPosition = position.WithY(UnityConstants.YOffset);

            if (IsAiming == false)
                return;

            Vector3 origin = _getTankPositionQuery.Handle().WithY(UnityConstants.YOffset);
            Vector3 direction = AimPosition - origin;
            direction = Vector3.ProjectOnPlane(direction, Vector3.up).normalized;

            if (Physics.Raycast(origin, direction, out RaycastHit hit, float.MaxValue, SLayerMask) == false)
            {
                EndAim(AimPosition);

                return;
            }

            AimPosition = hit.point;
            _aimView.SetPoints(origin, AimPosition);
        }

        public void EndAim(Vector3 position)
        {
            IsAiming = false;
            _aimView.Disable();
        }
    }
}
