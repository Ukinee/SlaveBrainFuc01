using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.MainMenu.Models;

namespace Codebase.Game.CQRS.Commands
{
    public class AddLevelToLevelSelectionFormCommand : EntityUseCaseBase<LevelSelectionFormModel>
    {
        public AddLevelToLevelSelectionFormCommand(IEntityRepository repository) : base(repository)
        {
        }

        public void Handle(int formId, int levelId) =>
            Get(formId).AddLevel(levelId);
    }
}
