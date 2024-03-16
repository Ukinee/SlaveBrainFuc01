using Codebase.Forms.Presentations.Interfaces;

namespace Codebase.MainMenu.Presentations.Interfaces
{
    public interface IMainMenuLeaderboardFormPresenter : IFormPresenter
    {
        public void OnBackClick();
        public void OnViewDisposed();
    }
}
