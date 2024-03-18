using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.PlayerData.Models;
using Codebase.PlayerData.Services.Interfaces;

namespace Codebase.PlayerData.CQRS.Commands
{
    public class AddUnlockedGamePresetCommand: EntityUseCaseBase<PlayerDataModel>
    {
        private readonly IPlayerIdProvider _playerIdProvider;

        public AddUnlockedGamePresetCommand(IPlayerIdProvider playerIdProvider, IEntityRepository repository) : base(repository)
        {
            _playerIdProvider = playerIdProvider;
        }

        public void Handle(string gamePresetId)
        {
            PlayerDataModel model = Get(_playerIdProvider.Id);

            model.AddUnlockedGamePreset(gamePresetId);
        }
    }
}
