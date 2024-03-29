﻿using ApplicationCode.Core.Services.Cameras;
using ApplicationCode.Core.Services.CursorWorldPositions;
using Codebase.Core.Services.Common;
using UnityEngine;

namespace ApplicationCode.Core.Services.RaycastHitProviders
{
    public class RaycastHitProvider : IRaycastHitProvider
    {
        private readonly CursorWorldPositionService _cursorWorldPositionService;

        public RaycastHitProvider(CameraService cameraService)
        {
            _cursorWorldPositionService = new CursorWorldPositionService(cameraService);
        }

        public bool HasHit { get; private set; }
        public Vector3 HitPoint { get; private set; }
        public RaycastHit Hit { get; private set; }

        public void OnUpdate(Vector3 mousePosition)
        {
            HasHit = _cursorWorldPositionService.TryGetRaycastHit(mousePosition, out RaycastHit hit);

            Hit = hit;
            HitPoint = hit.point;
        }
    }
}
