using Codebase.Core.Frameworks.MVP.Interfaces;

namespace Codebase.MainMenu.Presentations.Interfaces
{
    public interface IShopLevelPresenter : IPresenter
    {
        public void OnClick();
        public void OnViewDisposed();
    }
}
