using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Core.Frameworks.MVP.Interfaces;
using Codebase.Structures.Models;
using Codebase.Structures.Views.Interfaces;

namespace Codebase.Structures.Presentations.Implementations
{
    public class StructurePresenter : IPresenter
    {
        private StructureModel _structureModel;
        private IStructureView _structureView;
        private IAmountView _amountView;

        public StructurePresenter(StructureModel structureModel, IStructureView structureView, IAmountView amountView)
        {
            _structureModel = structureModel;
            _structureView = structureView;
            _amountView = amountView;
        }

        public void Enable()
        {
            _structureModel.AmountChanged += OnAmountChanged;
            _structureModel.Disposed += Dispose;
        }

        public void Disable()
        {
            _structureModel.AmountChanged -= OnAmountChanged;
            _structureModel.Disposed -= Dispose;
        }

        private void OnAmountChanged(int amount)
        {
            _amountView.Set($"{amount} / {_structureModel.MaxAmount}");
        }

        private void Dispose()
        {
            "Structure Presenter Dispose".Log();
            
            Disable();
            _structureView.Dispose();
            _amountView.Dispose();

            _structureModel = null;
            _structureView = null;
            _amountView = null;
        }
    }
}
