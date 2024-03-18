using Codebase.MainMenu.Models;
using Codebase.MainMenu.Services.Interfaces;
using Codebase.MainMenu.Views.Interfaces;
using Codebase.Maps.Common;

namespace Codebase.MainMenu.Services.Implementations.Repositories
{
    public class MapRepositoryController : IMapViewRepository, IMapTypeRepository
    {
        private MapModelRepository _mapModelRepository = new MapModelRepository();
        private MapViewRepository _mapViewRepository = new MapViewRepository();
        private MapTypeRepository _mapTypeRepository = new MapTypeRepository();

        public void Register(MainMenuMapModel model, IMainMenuMapView levelView)
        {
            int id = model.Id;

            _mapModelRepository.Register(id, model);
            _mapViewRepository.Register(id, levelView);
            _mapTypeRepository.Register(model.MapType, id);

            model.Disposed += Remove;
        }

        public IMainMenuMapView GetView(int id) =>
            _mapViewRepository.Get(id);
        
        public int Get(MapType mapType) =>
            _mapTypeRepository.Get(mapType);

        private void Remove(int id)
        {
            MainMenuMapModel model = _mapModelRepository.Get(id);
            model.Disposed -= Remove;

            _mapModelRepository.Remove(id);
            _mapViewRepository.Remove(id);
            _mapTypeRepository.Remove(model.MapType);
        }
    }
}
