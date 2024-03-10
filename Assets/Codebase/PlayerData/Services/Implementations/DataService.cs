using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Common.General.Utils;
using Codebase.PlayerData.CQRS.Queries;
using Codebase.PlayerData.Infrastructure.DTO;
using Codebase.PlayerData.Services.Interfaces;
using Unity.Plastic.Newtonsoft.Json;

namespace Codebase.PlayerData.Services.Implementations
{
    public class DataService : IDataService
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly IPlayerIdProvider _playerIdProvider;
        private readonly GetPlayerDataObjectQuery _getPlayerDataObjectQuery;
        private readonly string _dataKey;
        
        public DataService(ISaveLoadService saveLoadService, IPlayerIdProvider playerIdProvider, GetPlayerDataObjectQuery getPlayerDataObjectQuery)
        {
            _saveLoadService = saveLoadService;
            _playerIdProvider = playerIdProvider;
            _getPlayerDataObjectQuery = getPlayerDataObjectQuery;
            _dataKey = PathConstants.DataKey;
        }

        public PlayerDataObject Get()
        {
            if (_saveLoadService.HasSave(_dataKey) == false)
                return PlayerDataObject.Initial;
            
            string json = _saveLoadService.Load(_dataKey);
            PlayerDataObject dataObject = JsonConvert.DeserializeObject<PlayerDataObject>(json);

            if (dataObject.IsFirstStart)
                MaloyAlert.Error($"Data object loaded as first start. Data: ```{json}```");
            
            return dataObject;
        }

        public void Save()
        {
            PlayerDataObject dataObject = _getPlayerDataObjectQuery.Handle(_playerIdProvider.Id);
            
            string json = JsonConvert.SerializeObject(dataObject);
            
            _saveLoadService.Save(_dataKey, json);
        }

        public void Clear()
        {
            _saveLoadService.Delete(_dataKey);
        }
    }
}
