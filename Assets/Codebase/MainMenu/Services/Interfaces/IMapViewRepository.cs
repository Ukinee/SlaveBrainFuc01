using Codebase.MainMenu.Views.Interfaces;

namespace Codebase.MainMenu.Services.Interfaces
{
    public interface IMapViewRepository
    {
        public IMainMenuMapView GetView(int id);
    }
}
