using Codebase.Maps.Views.Interfaces;

namespace Codebase.Maps.Controllers.ServiceCommands
{
    public class SetObstacleServiceCommand
    {
        private readonly IMapView _mapView;

        public SetObstacleServiceCommand(IMapView mapView)
        {
            _mapView = mapView;
        }

        public void Set(string obstacleId)
        {
            _mapView.SetObstacle(obstacleId);
        }

        public void Hide()
        {
            _mapView.HideObstacle();
        }
    }
}
