using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Tank.Model;
using Codebase.Tank.Presentation.Implementations;
using Codebase.Tank.Services.Interfaces;
using Codebase.Tank.View.Implementations;

namespace Codebase.Tanks.Services.Implementations
{
    public class TankCreationService
    {
        private readonly ITankPositionCalculator _tankPositionCalculator;
        private readonly AssetProvider _assetProvider;
        private readonly string _path;

        public TankCreationService
        (
            ITankPositionCalculator tankPositionCalculator,
            FilePathProvider filePathProvider,
            AssetProvider assetProvider
        )
        {
            _tankPositionCalculator = tankPositionCalculator;
            _path = filePathProvider.General.Data[PathConstants.General.Tank];
            _assetProvider = assetProvider;
        }

        public TankModel Create()
        {
            TankModel tankModel = new TankModel();
            TankView tankView = _assetProvider.Instantiate<TankView>(_path);
            TankPositionPresenter tankPresenter = new TankPositionPresenter(tankModel, tankView, _tankPositionCalculator);

            tankPresenter.Enable();
            
            return tankModel;
        }
    }
}
