using Codebase.Core.Frameworks.MVP.Interfaces;
using Codebase.Tank.Model;
using Codebase.Tank.Services.Interfaces;
using Codebase.Tank.View.Interfaces;
using UnityEngine;

namespace Codebase.Tank.Presentation.Implementations
{
    public class TankPositionPresenter
    {
        private readonly TankModel _tankModel;
        private readonly ITankView _tankView;
        private readonly ITankPositionCalculator _tankPositionCalculator;

        public TankPositionPresenter(TankModel tankModel, ITankView tankView, ITankPositionCalculator tankPositionCalculator)
        {
            _tankModel = tankModel;
            _tankView = tankView;
            _tankPositionCalculator = tankPositionCalculator;
        }

        public void Enable()
        {
            _tankModel.PositionChanged += OnPositionChanged;
        }

        public void Disable()
        {
            _tankModel.PositionChanged -= OnPositionChanged;
        }

        private void OnPositionChanged(float position)
        {
            Vector3 tankPosition = _tankPositionCalculator.CalculatePosition(position);
            _tankView.SetPosition(tankPosition);
        }
    }
}
