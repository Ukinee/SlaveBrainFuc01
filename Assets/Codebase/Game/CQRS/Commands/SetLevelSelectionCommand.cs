using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.MainMenu.Models;

namespace Codebase.Game.CQRS.Commands
{
    public class SetLevelSelectionCommand : EntityUseCaseBase<LevelModel>
    {
        public SetLevelSelectionCommand(IEntityRepository repository) : base(repository)
        {
        }

        public void Handle(int id, bool value) =>
            Get(id).SetSelection(value);
    }
}
