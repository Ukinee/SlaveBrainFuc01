using System;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;

namespace Codebase.Core.Frameworks.EnitySystem.CQRS
{
    public class DisposeCommand : EntityUseCaseBase<IDisposable>
    {
        public DisposeCommand(IEntityRepository repository) : base(repository)
        {
        }

        public void Handle(int id)
        {
            Get(id).Dispose();
        }
    }
}
