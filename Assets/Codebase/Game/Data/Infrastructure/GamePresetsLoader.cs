using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Game.Data.Common;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Codebase.Game.Data.Infrastructure
{
    public class GamePresetsLoader
    {
        private readonly AssetProvider _assetProvider;
        private readonly string _gamePresetsPath;
        
        public GamePresetsLoader(AssetProvider assetProvider, FilePathProvider filePathProvider)
        {
            _assetProvider = assetProvider;
            _gamePresetsPath = filePathProvider.Game.Data[PathConstants.Game.GamePresets];
        }
        
        public GamePresets Load()
        { 
            string json = _assetProvider.Get<TextAsset>(_gamePresetsPath).text;
            
            return JsonConvert.DeserializeObject<GamePresets>(json) ?? throw new JsonException();
        }
    }
}
