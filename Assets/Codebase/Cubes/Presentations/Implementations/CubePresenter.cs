using Assets.Codebase.Core.Frameworks.SignalSystem.Interfaces;
using Codebase.Core.Common.Application.Types;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.SignalSystem.Common.Signals;
using Codebase.Cubes.Controllers.Signals;
using Codebase.Cubes.CQRS.Queries;
using Codebase.Cubes.Presentations.Interfaces;
using Codebase.Cubes.Views.Interfaces;

namespace Codebase.Cubes.Presentations.Implementations
{
    public class CubePresenter : ICubePresenter
    {
        private readonly int _id;
        private ILiveData<CubeColor> _color;
        private ISignalBus _signalBus;
        private ICubeView _cubeView;

        public CubePresenter(int id, ISignalBus signalBus, GetColorQuery getColorQuery, ICubeView cubeView)
        {
            _color = getColorQuery.Handle(id);
            _id = id;
            _signalBus = signalBus;
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
            _signalBus.Handle(new ActivateCubeSignal(_id));
            _cubeView.Activate();
        }

        private void OnColorChanged(CubeColor color) =>
            _cubeView.SetColor(color);

        public void Dispose()
        {
            _signalBus.Handle(new DisposeSignal(_id));
            
            _signalBus = null;
            _color = null;
            _cubeView = null;
        }
    }
}
