using Codebase.Maps.Common;

namespace Codebase.Maps.Views.Interfaces
{
    public interface IMapView
    {
        public void ShowMap(MapType mapType);
        public void SetObstacle(string obstacleId);
        public void HideObstacle();
    }
}
