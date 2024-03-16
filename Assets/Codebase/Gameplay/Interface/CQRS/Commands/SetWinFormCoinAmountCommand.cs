using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Gameplay.Interface.Models;

namespace Codebase.Gameplay.Interface.CQRS
{
    public class SetWinFormCoinAmountCommand : EntityUseCaseBase<WinFormModel>
    {
        public SetWinFormCoinAmountCommand(IEntityRepository repository) : base(repository)
        {
        }

        public void Handle(int id, int amount)
        {
            Get(id).SetCoinAmount(amount);
        }
    }
}
