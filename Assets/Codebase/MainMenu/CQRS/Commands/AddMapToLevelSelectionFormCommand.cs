using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.MainMenu.Models;

namespace Codebase.MainMenu.CQRS.Commands
{
    public class AddMapToLevelSelectionFormCommand : EntityUseCaseBase<MainMenuLevelSelectorFormModel>
    {
        public AddMapToLevelSelectionFormCommand(IEntityRepository repository) : base(repository)
        {
        }

        public void Handle(int formId, int mapId) =>
            Get(formId).AddMap(mapId);
    }
}
