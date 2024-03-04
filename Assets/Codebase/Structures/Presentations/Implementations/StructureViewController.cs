using Codebase.Structures.Models;
using Codebase.Structures.Views.Interfaces;

namespace Codebase.Structures.Presentations.Implementations
{
    public class StructureViewController
    {
        private StructureModel _structureModel;
        private IStructureView _structureView;

        public StructureViewController(StructureModel structureModel, IStructureView structureView)
        {
            _structureModel = structureModel;
            _structureView = structureView;
        }

        public void Enable()
        {
            _structureModel.Disposed += Dispose;
        }

        private void Dispose(int id)
        {
            _structureModel.Disposed -= Dispose;
            _structureView.Dispose();

            _structureModel = null;
            _structureView = null;
        }
    }
}
