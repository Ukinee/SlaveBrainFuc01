using Codebase.Core.Common.Application.Types;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Cubes.CQRS.Queries;
using Codebase.Cubes.Presentations.Interfaces;
using Codebase.Cubes.Views.Interfaces;
using Codebase.Gameplay.Cubes.Controllers.ServiceCommands;
using Codebase.Structures.Services.Interfaces;

namespace Codebase.Gameplay.Cubes.Presentations.Implementations
{
    public class CubePresenter : ICubePresenter
    {
        private readonly int _id;
        private IStructureService _structureService;
        private CubeDeactivatorCollisionHandler _cubeDeactivatorCollisionHandler;
        private ILiveData<CubeColor> _color;
        private ICubeView _cubeView;

        public CubePresenter
        (
            int id,
            IStructureService structureService,
            CubeDeactivatorCollisionHandler cubeDeactivatorCollisionHandler,
            GetColorQuery getColorQuery,
            ICubeView cubeView
        )
        {
            _id = id;
            _structureService = structureService;
            _cubeDeactivatorCollisionHandler = cubeDeactivatorCollisionHandler;
            _color = getColorQuery.Handle(id);
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
            _cubeDeactivatorCollisionHandler.Handle(_id);

            _cubeDeactivatorCollisionHandler = null;
            _structureService = null;
            _color = null;
            _cubeView = null;
        }
    }
}
