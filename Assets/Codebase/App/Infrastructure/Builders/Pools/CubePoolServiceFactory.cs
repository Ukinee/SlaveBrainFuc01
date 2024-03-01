using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.Utils;
using Codebase.Cubes.Services.Implementations;

namespace Codebase.App.Infrastructure.Builders.Pools
{
    public class CubePoolServiceFactory
    {
        public CubePoolServiceFactory(AssetProvider assetProvider, FilePathProvider filePathProvider)
        {
            _assetProvider = assetProvider;
            _filePathProvider = filePathProvider;
        }

        private readonly AssetProvider _assetProvider;
        private readonly FilePathProvider _filePathProvider;

        public CubePoolService Create()
        {
            return new CubePoolService(new CubePool(new CubeViewFactory(_assetProvider, _filePathProvider).Create));
        }
    }
}
