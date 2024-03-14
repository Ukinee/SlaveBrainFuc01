using Codebase.Core.Frameworks.MVP.Interfaces;

namespace Codebase.Game.Presentations.Interfaces
{
    public interface ILevelSelectingFormPresenter : IPresenter
    {
        public void OnStartClicked();
        public void OnBackClicked();
    }
}
