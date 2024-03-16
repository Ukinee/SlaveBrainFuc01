using Codebase.Game.Views.Interfaces;
using Codebase.MainMenu.Models;
using Codebase.MainMenu.Services.Interfaces;

namespace Codebase.MainMenu.Services.Implementations.Repositories
{
    public class LevelRepositoryController : ILevelViewRepository
    {
        private LevelModelRepository _levelModelRepository = new LevelModelRepository();
        private LevelViewRepository _levelViewRepository = new LevelViewRepository();

        public void Register(LevelModel model, ILevelView levelView)
        {
            int id = model.Id;
            
            _levelModelRepository.Register(id, model);
            _levelViewRepository.Register(id, levelView);

            model.Disposed += Remove;
        }

        public void Remove(int id)
        {
            LevelModel model = _levelModelRepository.Get(id);
            model.Disposed -= Remove;
            
            _levelModelRepository.Remove(id);
            _levelViewRepository.Remove(id);
        }

        public ILevelView GetView(int id) =>
             _levelViewRepository.Get(id);
    }
}
