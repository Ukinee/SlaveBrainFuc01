using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Gameplay.PlayerData.CQRS.Commands;

namespace Codebase.Gameplay.Cubes.Controllers.ServiceCommands
{
    public class CubeDeactivatorCollisionHandler
    {
        private DisposeCommand _disposeCommand;
        private AddGameplayCoinsCommand _addGameplayCoinsCommand;

        public CubeDeactivatorCollisionHandler
        (
            IEntityRepository entityRepository,
            AddGameplayCoinsCommand addGameplayCoinsCommand
        )
        {
            _disposeCommand = new DisposeCommand(entityRepository);
            _addGameplayCoinsCommand = addGameplayCoinsCommand;
        }

        public void Handle(int cubeId)
        {
            _addGameplayCoinsCommand.Handle(50); //todo : hardcoded value
            _disposeCommand.Handle(cubeId);
            _serviceUpgradeService.OnCubeDeactivated();
        }
    }
}
