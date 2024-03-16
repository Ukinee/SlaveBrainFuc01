namespace Codebase.Forms.Presentations.Interfaces.MainMenu
{
    public interface IMainMenuSettingsFormPresenter : IFormPresenter
    {
        public void OnClickBack();
        public void OnClickSound();
        public void OnClickMusic();
        public void OnViewDisposed();
    }
}
