using Codebase.Core.Frameworks.MVP.Interfaces;

namespace Codebase.MainMenu.Presentations.Interfaces
{
    public interface ILevelPresenter : IPresenter
    {
        public void OnSelectButtonClick();
        public void OnBuyButtonClick();
        public void OnViewDispose();
    }
}
