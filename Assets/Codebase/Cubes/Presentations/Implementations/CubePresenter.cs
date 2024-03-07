using Codebase.Core.Common.Application.Types;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Cubes.CQRS.Queries;
using Codebase.Cubes.Presentations.Interfaces;
using Codebase.Cubes.Views.Interfaces;
using Codebase.Structures.Services.Interfaces;

namespace Codebase.Cubes.Presentations.Implementations
{
    public class CubePresenter : ICubePresenter
    {
        private readonly int _id;
        private IStructureService _structureService;
        private DisposeCommand _disposeCommand;
        private ILiveData<CubeColor> _color;
        private ICubeView _cubeView;

        public CubePresenter
        (
            int id,
            IStructureService structureService,
            DisposeCommand disposeCommand,
            GetColorQuery getColorQuery,
            ICubeView cubeView
        )
        {
            _id = id;
            _structureService = structureService;
            _color = getColorQuery.Handle(id);
            _disposeCommand = disposeCommand;
            _cubeView = cubeView;
        }

        public void Enable() =>
            _color.AddListener(OnColorChanged);

        public void Disable() =>
            _color.RemoveListener(OnColorChanged);

        public void OnDeactivatorCollision() =>
            Dispose();

        public void OnBallCollision()
        {
            _cubeView.Activate();
            _structureService.RemoveCube(_id);
        }

        private void OnColorChanged(CubeColor color) =>
            _cubeView.SetColor(color);

        public void Dispose()
        {
            _disposeCommand.Handle(_id);

            _structureService = null;
            _disposeCommand = null;
            _color = null;
            _cubeView = null;
        }
    }
}
