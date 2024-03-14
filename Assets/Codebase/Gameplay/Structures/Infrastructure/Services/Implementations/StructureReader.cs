using System;
using System.IO;
using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Structures.Common;
using Codebase.Structures.Infrastructure.Services.Interfaces;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Codebase.Structures.Infrastructure.Services.Implementations
{
    public class StructureReader : IStructureReader
    {
        private readonly AssetProvider _assetProvider;
        private readonly string _directoryPath;
        
        public StructureReader(AssetProvider assetProvider, FilePathProvider filePathProvider)
        {
            _assetProvider = assetProvider;
            _directoryPath = filePathProvider.Structures.Data[PathConstants.Structures.StructureDirectoryKey];
        }

        public StructureDto Read(string structureName)
        {
            string fullPath = Path.Combine(_directoryPath, structureName);
            
            string json = _assetProvider.Get<TextAsset>(fullPath).text;
            return JsonConvert.DeserializeObject<StructureDto>(json) ?? throw new NullReferenceException();
        }
    }
}
