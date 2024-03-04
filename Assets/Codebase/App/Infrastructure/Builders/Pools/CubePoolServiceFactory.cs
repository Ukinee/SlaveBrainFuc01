using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.Utils;
using Codebase.Cubes.Services.Implementations;

namespace Codebase.App.Infrastructure.Builders.Pools
{
    public class CubePoolServiceFactory
    {
        public CubePoolServiceFactory(AssetProvider assetProvider, FilePathProvider filePathProvider, CubeRepository cubeRepository)
        {
            _assetProvider = assetProvider;
            _filePathProvider = filePathProvider;
            _cubeRepository = cubeRepository;
        }

        private readonly AssetProvider _assetProvider;
        private readonly FilePathProvider _filePathProvider;
        private readonly CubeRepository _cubeRepository;

        public CubePoolService Create()
        {
            return new CubePoolService(new CubePool(_cubeRepository, new CubeViewFactory(_assetProvider, _filePathProvider).Create), _cubeRepository);
        }
    }
}
