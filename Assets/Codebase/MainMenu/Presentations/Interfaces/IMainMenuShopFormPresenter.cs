using Codebase.Core.Frameworks.MVP.Interfaces;

namespace Codebase.MainMenu.Presentations.Interfaces
{
    public interface IMainMenuShopFormPresenter : IPresenter
    {
        public void OnClickBack();
        public void OnViewDisposed();
    }
}
