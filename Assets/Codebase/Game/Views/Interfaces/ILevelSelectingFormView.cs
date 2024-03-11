using Codebase.Forms.Views.Interfaces;

namespace Codebase.Game.Views.Interfaces
{
    public interface ILevelSelectingFormView : IFormView
    {
        public void SetParent(ILevelView levelView);
    }
}
