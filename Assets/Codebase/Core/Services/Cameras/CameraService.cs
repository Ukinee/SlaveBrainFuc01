using UnityEngine;

namespace ApplicationCode.Core.Services.Cameras
{
    public class CameraService
    {
        public Camera Camera { get; private set; }

        public void Set(Camera camera)
        {
            Camera = camera;
        }
    }
}