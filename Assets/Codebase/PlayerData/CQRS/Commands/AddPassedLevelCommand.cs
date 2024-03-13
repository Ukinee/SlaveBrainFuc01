using System;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.PlayerData.Models;
using Codebase.PlayerData.Services.Interfaces;

namespace Codebase.PlayerData.CQRS.Commands
{
    public class AddPassedLevelCommand : EntityUseCaseBase<PlayerDataModel>
    {
        private readonly IPlayerIdProvider _playerIdProvider;

        public AddPassedLevelCommand(IPlayerIdProvider playerIdProvider, IEntityRepository repository) : base(repository)
        {
            _playerIdProvider = playerIdProvider;
        }

        public void Handle(string levelId)
        {
            PlayerDataModel model = Get(_playerIdProvider.Id);

            model.AddPassedLevel(levelId);
        }
    }
}
