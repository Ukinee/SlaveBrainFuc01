using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Assets.Codebase.Core.Frameworks.SignalSystem.Interfaces.SignalActions.Generic;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Core.Frameworks.SignalSystem.Common.Signals;

namespace Codebase.Core.Frameworks.SignalSystem.Common.SignalActions
{
    public class DisposeSignalAction : ISignalAction<DisposeSignal>
    {
        private readonly DisposeCommand _disposeCommand;

        public DisposeSignalAction(IEntityRepository entityRepository)
        {
            _disposeCommand = new DisposeCommand(entityRepository);
        }

        public void Handle(DisposeSignal signal)
        {
            _disposeCommand.Handle(signal.Id);
        }
    }
}
