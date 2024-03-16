using Codebase.Core.Frameworks.MVP.Interfaces;

namespace Codebase.Forms.Presentations.Interfaces.MainMenu
{
    public interface IMainMenuShopFormPresenter : IPresenter
    {
        public void OnClickBack();
        public void OnViewDisposed();
    }
}
