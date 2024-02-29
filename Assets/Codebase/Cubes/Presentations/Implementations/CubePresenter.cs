using Codebase.Core.Common.Application.Types;
using Codebase.Cubes.Models;
using Codebase.Cubes.Presentations.Interfaces;
using Codebase.Cubes.Views.Interfaces;

namespace Codebase.Cubes.Presentations.Implementations
{
    public class CubePresenter : ICubePresenter
    {
        private CubeModel _cubeModel;
        private ICubeView _cubeView;

        public CubePresenter(CubeModel cubeModel, ICubeView cubeView)
        {
            _cubeModel = cubeModel;
            _cubeView = cubeView;
        }
        
        public void Enable()
        {
            _cubeModel.ColorChanged += OnColorChanged;
            OnColorChanged(_cubeModel.Color);
        }

        public void Disable()
        {
            _cubeModel.ColorChanged -= OnColorChanged;
        }

        public void SetColor(CubeColor color) =>
            _cubeModel.SetColor(color);

        public void OnBallCollision() =>
            _cubeView.Activate();

        public void OnDeactivatorCollision()
        {
            _cubeView.Deactivate();
            Dispose();
        }

        private void OnColorChanged(CubeColor color) =>
            _cubeView.SetColor(color);

        public void Dispose()
        {
            Disable();
            _cubeView.ReturnToPool();
            _cubeModel.Destroy();
            _cubeView = null;
            _cubeModel = null;
        }
    }
}
