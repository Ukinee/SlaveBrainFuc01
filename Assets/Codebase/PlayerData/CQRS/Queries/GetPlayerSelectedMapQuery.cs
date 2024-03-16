using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Maps.Common;
using Codebase.PlayerData.Models;
using Codebase.PlayerData.Services.Interfaces;

namespace Codebase.PlayerData.CQRS.Queries
{
    public class GetPlayerSelectedMapQuery : EntityUseCaseBase<PlayerDataModel>
    {
        private readonly IPlayerIdProvider _playerIdProvider;

        public GetPlayerSelectedMapQuery(IPlayerIdProvider playerIdProvider, IEntityRepository repository) : base(repository)
        {
            _playerIdProvider = playerIdProvider;
        }

        public MapType Handle()
        {
            return Get(_playerIdProvider.Id).SelectedMap;
        }
    }
}
