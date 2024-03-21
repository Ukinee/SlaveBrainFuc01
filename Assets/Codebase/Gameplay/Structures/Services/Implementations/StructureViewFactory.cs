using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Structures.Views.Implementations;

namespace Codebase.Gameplay.Structures.Services.Implementations
{
    public class StructureViewFactory
    {
        private readonly AssetProvider _assetProvider;
        private readonly string _structureViewPath;

        public StructureViewFactory
        (
            AssetProvider assetProvider,
            FilePathProvider filePathProvider
        )
        {
            _assetProvider = assetProvider;
            _structureViewPath = filePathProvider.Structures.Data[PathConstants.Structures.Structure];
        }

        public StructureView Create()
        {
            return _assetProvider.Instantiate<StructureView>(_structureViewPath);
        }
    }
}
