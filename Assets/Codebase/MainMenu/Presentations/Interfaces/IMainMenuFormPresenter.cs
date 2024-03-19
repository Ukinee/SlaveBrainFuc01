using Codebase.Core.Frameworks.MVP.Interfaces;
using Codebase.Maps.Common;

namespace Codebase.MainMenu.Presentations.Interfaces
{
    public interface IMainMenuFormPresenter : IPresenter
    {
        public void OnClickSettings();
        public void OnClickLeaderboard();
        public void OnClickLevelSelection();
        public void OnViewDisposed();
    }
}
