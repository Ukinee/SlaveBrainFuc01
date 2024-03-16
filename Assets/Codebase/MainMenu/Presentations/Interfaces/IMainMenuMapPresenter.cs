using Codebase.Core.Frameworks.MVP.Interfaces;

namespace Codebase.MainMenu.Presentations.Interfaces
{
    public interface IMainMenuMapPresenter : IPresenter
    {
        public void OnButtonClick();
        public void OnViewDisposed();
    }
}
