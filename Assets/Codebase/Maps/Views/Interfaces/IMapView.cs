using Codebase.Maps.Common;
using UnityEngine;

namespace Codebase.Maps.Views.Interfaces
{
    public interface IMapView
    {
        public Vector3 TankLeftPosition { get; }
        public Vector3 TankRightPosition { get; }
        public Vector3 TankVerticalPosition { get; }

        public void ShowMap(MapType mapType);
    }
}
