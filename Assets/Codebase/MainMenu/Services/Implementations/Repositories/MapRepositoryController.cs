using Codebase.MainMenu.Models;
using Codebase.MainMenu.Services.Interfaces;
using Codebase.MainMenu.Views.Interfaces;

namespace Codebase.MainMenu.Services.Implementations.Repositories
{
    public class MapRepositoryController : IMapViewRepository
    {
        private MapModelRepository _mapModelRepository = new MapModelRepository();
        private MapViewRepository _mapViewRepository = new MapViewRepository();

        public void Register(MainMenuMapModel model, IMainMenuMapView levelView)
        {
            int id = model.Id;
            
            _mapModelRepository.Register(id, model);
            _mapViewRepository.Register(id, levelView);

            model.Disposed += Remove;
        }

        private void Remove(int id)
        {
            MainMenuMapModel model = _mapModelRepository.Get(id);
            model.Disposed -= Remove;
            
            _mapModelRepository.Remove(id);
            _mapViewRepository.Remove(id);
        }

        public IMainMenuMapView GetView(int id) =>
            _mapViewRepository.Get(id);
    }
}
