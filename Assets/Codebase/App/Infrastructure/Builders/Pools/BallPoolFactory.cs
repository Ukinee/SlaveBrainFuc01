using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Balls.Services.Implementations;
using Codebase.Core.Common.Application.Utils;

namespace Codebase.App.Infrastructure.Builders.Pools
{
    public class BallPoolFactory
    {
        private AssetProvider _assetProvider;

        private FilePathProvider _filePathProvider;

        public BallPoolFactory(AssetProvider assetProvider, FilePathProvider filePathProvider)
        {
            _assetProvider = assetProvider;
            _filePathProvider = filePathProvider;
        }

        public BallPool Create()
        {
            CollisionService collisionService = new CollisionService();
            MoveService moveService = new MoveService();

            BallCreationService ballCreationService = new BallCreationService
                (_assetProvider, _filePathProvider, collisionService, moveService);

            return new BallPool(ballCreationService.Create);
        }
    }
}
