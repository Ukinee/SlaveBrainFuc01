using Codebase.Core.Frameworks.MVP.Interfaces;
using Codebase.Maps.Common;

namespace Codebase.Forms.Presentations.Interfaces.MainMenu
{
    public interface IMainMenuFormPresenter : IPresenter
    {
        public void OnClickSettings();
        public void OnClickLeaderboard();
        public void OnClickShop();
        public void OnClickMapType(MapType mapType);
        public void OnClickLevelSelection();
        public void OnViewDisposed();
    }
}
