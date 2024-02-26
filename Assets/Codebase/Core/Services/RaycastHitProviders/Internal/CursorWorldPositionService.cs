using ApplicationCode.Core.Common.General.Utils;
using ApplicationCode.Core.Services.Cameras;
using UnityEngine;

namespace ApplicationCode.Core.Services.CursorWorldPositions
{
    internal class CursorWorldPositionService
    {
        private readonly CameraService _cameraService;

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
            
            return Physics.Raycast(ray, out hit, float.MaxValue);
        }
    }
}