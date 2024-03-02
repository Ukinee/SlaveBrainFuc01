using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Balls.Views.Implementations;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;

namespace Codebase.App.Infrastructure.Builders.Pools
{
    public class BallViewFactory
    {
        private readonly AssetProvider _assetProvider;
        private readonly string _path;

        public BallViewFactory(AssetProvider assetProvider, FilePathProvider filePathProvider)
        {
            _assetProvider = assetProvider;
            _path = filePathProvider.General.Data[PathConstants.General.Ball];
        }
        
        public BallView Create()
        {
            return _assetProvider.Instantiate<BallView>(_path);
        }
    }
}
