using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.PoolTags;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Cubes.Models;
using Codebase.Cubes.Views.Implementations;
using UnityEngine;

namespace Codebase.Cubes.Services.Implementations
{
    public class CubeViewFactory
    {
        private readonly AssetProvider _assetProvider;
        private readonly string _path;

        public CubeViewFactory(AssetProvider assetProvider, FilePathProvider filePathProvider)
        {
            _assetProvider = assetProvider;
            _path = filePathProvider.General.Data[PathConstants.General.Cube];


        }

        public CubeView Create()
        {
            CubeView cubeView = _assetProvider.Instantiate<CubeView>(_path);
            
            return cubeView;
        }
    }
}
