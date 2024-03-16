using Codebase.Forms.Presentations.Interfaces;

namespace Codebase.MainMenu.Presentations.Interfaces
{
    public interface IMainMenuSettingsFormPresenter : IFormPresenter
    {
        public void OnClickBack();
        public void OnClickSound();
        public void OnClickMusic();
        public void OnViewDisposed();
    }
}
