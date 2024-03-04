using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Assets.Codebase.Core.Frameworks.SignalSystem.Interfaces.SignalActions.Generic;
using Codebase.Cubes.Controllers.Signals;
using Codebase.Cubes.CQRS.Commands;

namespace Codebase.Cubes.Controllers.Actions
{
    public class SetCubeColorSignalAction : ISignalAction<SetCubeColorSignal>
    {
        private SetColorCommand _colorCommand;

        public SetCubeColorSignalAction(IEntityRepository entityRepository)
        {
            _colorCommand = new SetColorCommand(entityRepository);
        }

        public void Handle(SetCubeColorSignal signal)
        {
            _colorCommand.Handle(signal.ID, signal.CubeColor);
        }
    }
}
