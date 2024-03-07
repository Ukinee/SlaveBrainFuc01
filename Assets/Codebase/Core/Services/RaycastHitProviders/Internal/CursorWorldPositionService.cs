using ApplicationCode.Core.Services.Cameras;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Common.General.Utils;
using UnityEngine;

namespace ApplicationCode.Core.Services.CursorWorldPositions
{
    internal class CursorWorldPositionService
    {
        private readonly CameraService _cameraService;
        
        private static readonly int SLayerMask = 1 << LayerMask.NameToLayer(GameConstants.RaycastTarget);

        public CursorWorldPositionService(CameraService cameraService)
        {
            _cameraService = cameraService;
        }
        
        public bool TryGetRaycastHit(Vector2 mousePosition, out RaycastHit hit)
        {
            if (_cameraService.Camera is null)
            {
                MaloyAlert.Error($"{nameof(_cameraService.Camera)} is null on {nameof(CursorWorldPositionService)}");
                hit = default;
                return false;
            }
            
            Ray ray = _cameraService.Camera.ScreenPointToRay(mousePosition);
            
            //todo: raycast layer
            
            return Physics.Raycast(ray, out hit, float.MaxValue, SLayerMask);
        }
    }
}