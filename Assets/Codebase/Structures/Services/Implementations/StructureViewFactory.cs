using ApplicationCode.Core.Services.AssetProviders;
using Codebase.Core.Common.Application.Utils;
using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Structures.Views.Implementations;

namespace Codebase.Structures.Services.Implementations
{
    public class StructureViewFactory
    {
        private readonly AssetProvider _assetProvider;
        private readonly string _structureViewPath;
        private readonly string _amountViewPath;

        public StructureViewFactory
        (
            AssetProvider assetProvider,
            FilePathProvider filePathProvider
        )
        {
            _assetProvider = assetProvider;
            _structureViewPath = filePathProvider.Structures.Data[PathConstants.Structures.Structure];
            _amountViewPath = filePathProvider.Structures.Data[PathConstants.Structures.AmountView];
        }

        public (StructureView structureView, AmountView amountView) CreateViews()
        {
            StructureView structureView = _assetProvider.Instantiate<StructureView>(_structureViewPath);
            AmountView amountView = _assetProvider.Instantiate<AmountView>(_amountViewPath);

            return (structureView, amountView);
        }
    }
}
