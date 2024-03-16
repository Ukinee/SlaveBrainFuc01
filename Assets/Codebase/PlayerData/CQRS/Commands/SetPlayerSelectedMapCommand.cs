using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Maps.Common;
using Codebase.PlayerData.Models;
using Codebase.PlayerData.Services.Interfaces;

namespace Codebase.PlayerData.CQRS.Commands
{
    public class SetPlayerSelectedMapCommand : EntityUseCaseBase<PlayerDataModel>
    {
        private readonly IPlayerIdProvider _playerIdProvider;

        public SetPlayerSelectedMapCommand(IPlayerIdProvider playerIdProvider, IEntityRepository repository) : base(repository)
        {
            _playerIdProvider = playerIdProvider;
        }

        public void Handle(MapType mapType)
        {
            Get(_playerIdProvider.Id).SetSelectedMap(mapType);
        }
    }
}
