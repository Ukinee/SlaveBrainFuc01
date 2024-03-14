using Codebase.Core.Frameworks.MVP.Interfaces;

namespace Codebase.Gameplay.Interface.Presentation.Interfaces
{
    public interface IPauseFormPresenter : IPresenter
    {
        public void OnResumeButtonPressed();
        public void OnMusicButtonPressed();
        public void OnSoundButtonPressed();
        public void OnExitButtonPressed();
    }
}
