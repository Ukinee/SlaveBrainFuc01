using System.Collections.Generic;
using System.Linq;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Common.General.Utils;
using Codebase.MainMenu.Common;
using Codebase.Maps.Common;
using Codebase.PlayerData.CQRS.Queries;
using Codebase.PlayerData.Services.Interfaces;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Codebase.MainMenu.DataServices
{
    public class ShopDataLoader
    {
        private readonly AssetProvider _assetProvider;
        private readonly string _filePath;

        private readonly GetPlayerGamePresetsQuery _getPlayerGamePresetsQuery;

        public ShopDataLoader
        (
            IEntityRepository entityRepository,
            AssetProvider assetProvider,
            FilePathProvider filePathProvider,
            IPlayerIdProvider playerDataService
        )
        {
            _assetProvider = assetProvider;
            _getPlayerGamePresetsQuery = new GetPlayerGamePresetsQuery(playerDataService, entityRepository);
            _filePath = filePathProvider.MainMenu.Data[PathConstants.MainMenu.ShopData];
        }

        public ShopData Load(string[] availableGamePresets, MapType[] availableMapTypes)
        {
            string json = _assetProvider.Get<TextAsset>(_filePath).text;
            ShopData shopData = JsonConvert.DeserializeObject<ShopData>(json);

            FilterGamePresets(shopData, availableGamePresets);
            FilterMapTypes(shopData, availableMapTypes);
            
            if (shopData.Structures.Count != availableGamePresets.Length || shopData.Maps.Count != availableMapTypes.Length)
                throw new System.InvalidOperationException("Failed to load all required shop data");

            return shopData;
        }

        private void FilterGamePresets(ShopData shopData, string[] availableGamePresets)
        {
            int amount = shopData.Structures.RemoveAll(data => availableGamePresets.Contains(data.Id) == false);
            
            MaloyAlert.Message($"During loading shop data, removed {amount} structures that are not available or unlocked");
        }

        private void FilterMapTypes(ShopData shopData, MapType[] availableMapTypes)
        {
            int amount = shopData.Maps.RemoveAll(data => availableMapTypes.Contains(data.Type) == false);
            
            MaloyAlert.Message($"During loading shop data, removed {amount} maps that are not available");
        }
    }
}
