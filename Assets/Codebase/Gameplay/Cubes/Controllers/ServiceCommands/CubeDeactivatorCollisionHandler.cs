using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Gameplay.PlayerData.CQRS.Commands;
using Codebase.Gameplay.Shooting.CQRS.Commands;

namespace Codebase.Gameplay.Cubes.Controllers.ServiceCommands
{
    public class CubeDeactivatorCollisionHandler
    {
        private DisposeCommand _disposeCommand;
        private AddGameplayCoinsCommand _addGameplayCoinsCommand;
        private AddShootingUpgradePointCommand _addShootingUpgradePointCommand;

        public CubeDeactivatorCollisionHandler
        (
            IEntityRepository entityRepository,
            AddGameplayCoinsCommand addGameplayCoinsCommand,
            AddShootingUpgradePointCommand addShootingUpgradePointCommand
        )
        {
            _disposeCommand = new DisposeCommand(entityRepository);
            _addGameplayCoinsCommand = addGameplayCoinsCommand;
            _addShootingUpgradePointCommand = addShootingUpgradePointCommand;
        }

        public void Handle(int cubeId)
        {
            _addShootingUpgradePointCommand.Handle();
            _addGameplayCoinsCommand.Handle(50); //todo : hardcoded value
            _disposeCommand.Handle(cubeId);
        }
    }
}
