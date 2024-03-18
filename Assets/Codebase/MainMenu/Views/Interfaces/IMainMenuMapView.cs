using Codebase.Maps.Common;

namespace Codebase.MainMenu.Views.Interfaces
{
    public interface IMainMenuMapView : ILevelSelectorPartView
    {
        public void SetMapType(MapType type);
        public void SetSelection(bool value);
        public void SetBought(bool isAvailable);
        public void SetPrice(int price);

    }
}
