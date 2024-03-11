using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Game.Models;

namespace Codebase.Game.CQRS.Commands
{
    public class PassLevelCommand : EntityUseCaseBase<LevelModel>
    {
        protected PassLevelCommand(IEntityRepository repository) : base(repository)
        {
        }
        
        public void Handle(int id) => 
            Get(id).SetPassed(true);
    }
}
