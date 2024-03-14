using Codebase.Game.Views.Interfaces;

namespace Codebase.MainMenu.Services.Interfaces
{
    public interface ILevelViewRepository
    {
        public ILevelView GetView(int id);
    }
}
