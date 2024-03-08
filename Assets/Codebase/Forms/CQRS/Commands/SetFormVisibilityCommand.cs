using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Forms.Models;

namespace Codebase.Forms.CQRS
{
    public class SetFormVisibilityCommand : EntityUseCaseBase<FormBase>
    {
        public SetFormVisibilityCommand(IEntityRepository repository) : base(repository)
        {
        }

        public void Handle(int id, bool isVisible)
        {
            Get(id).SetVisibility(isVisible);
        }
    }
}
