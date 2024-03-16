using Codebase.Core.Frameworks.MVP.Interfaces;

namespace Codebase.MainMenu.Presentations.Interfaces
{
    public interface ILevelSelectingFormPresenter : IPresenter
    {
        public void OnStartClicked();
        public void OnBackClicked();
        public void OnViewDispose();
    }
}
