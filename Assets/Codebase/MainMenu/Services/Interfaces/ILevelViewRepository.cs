using Codebase.MainMenu.Views.Interfaces;

namespace Codebase.MainMenu.Services.Interfaces
{
    public interface ILevelViewRepository
    {
        public ILevelView GetView(int id);
    }
}
