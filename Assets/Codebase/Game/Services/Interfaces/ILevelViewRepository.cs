using Codebase.Game.Views.Interfaces;

namespace Codebase.Game.Services.Interfaces
{
    public interface ILevelViewRepository
    {
        public ILevelView GetView(int id);
    }
}
