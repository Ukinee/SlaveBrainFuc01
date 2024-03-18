using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.MainMenu.Models;

namespace Codebase.MainMenu.CQRS.Commands
{
    public class MainMenuMapSetBoughtCommand : EntityUseCaseBase<MainMenuMapModel>
    {
        public MainMenuMapSetBoughtCommand(IEntityRepository repository) : base(repository)
        {
        }

        public void Handle(int id, bool value) =>
            Get(id).SetBought(value);
    }
}
