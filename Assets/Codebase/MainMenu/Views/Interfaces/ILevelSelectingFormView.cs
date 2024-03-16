using Codebase.Forms.Views.Interfaces;

namespace Codebase.MainMenu.Views.Interfaces
{
    public interface ILevelSelectingFormView : IFormView
    {
        public void AddLevel(ILevelView view);
        public void AddMap(IMainMenuMapView view);
    }
}
