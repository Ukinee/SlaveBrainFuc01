using System;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Gameplay.PlayerData.Services.Interfaces;

namespace Codebase.Gameplay.PlayerData.Services.Implementations
{
    public class GameplayPlayerDataService : IGameplayPlayerDataService
    {
        private readonly DisposeCommand _disposeCommand;

        public GameplayPlayerDataService(IEntityRepository entityRepository, int id)
        {
            Id = id;
            _disposeCommand = new DisposeCommand(entityRepository);
        }

        public int Id { get; }

        public void Dispose()
        {
            $"{nameof(GameplayPlayerDataService)}: {Id} is disposed".Log();
            _disposeCommand.Handle(Id);
        }
    }
}
