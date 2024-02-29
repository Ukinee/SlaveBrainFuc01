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
            _cubeView.Enable();
            
            _cubeModel.ColorChanged += OnColorChanged;
            OnColorChanged(_cubeModel.Color);
        }

        public void Disable()
        {
            _cubeView.Disable();
            _cubeModel.ColorChanged -= OnColorChanged;
        }

        public void SetColor(CubeColor color) =>
            _cubeModel.SetColor(color);

        public void Activate() =>
            _cubeView.Activate();

        public void Deactivate() =>
            _cubeView.Deactivate();

        private void OnColorChanged(CubeColor color) =>
            _cubeView.SetColor(color);

        public void Dispose()
        {
            _cubeView.ReturnToPool();
            _cubeModel.ColorChanged -= OnColorChanged;
            _cubeView = null;
            _cubeModel = null;
        }
    }
}
