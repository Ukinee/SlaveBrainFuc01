using Codebase.Core.Common.Application.Utils.Constants;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Core.Services.PauseServices;
using Codebase.Forms.Common.FormTypes.Gameplay;
using Codebase.Forms.Services.Implementations;
using Codebase.Gameplay.Interface.Presentation.Interfaces;
using Codebase.Gameplay.Interface.Views.Interfaces;
using Codebase.Gameplay.PlayerData.CQRS.Queries;
using Codebase.Gameplay.Shooting.CQRS.Queries;

namespace Codebase.Gameplay.Interface.Presentation.Implementations
{
    public class InterfaceFormPresenter : IInterfaceFormPresenter
    {
        private readonly int _id;
        private PauseService _pauseService;
        private IInterfaceService _interfaceService;
        private IInterfaceFormView _view;
        private DisposeCommand _disposeCommand;
        private ILiveData<int> _coinAmount;
        private ILiveData<int> _ballsToShoot;
        private ILiveData<int> _upgradePoints;

        private int _currentValue;

        public InterfaceFormPresenter
        (
            int id,
            PauseService pauseService,
            GetGameplayPlayerCoinAmountQuery coinAmountQuery,
            GetBallsToShootQuery getBallsToShootQuery,
            GetUpgradePointsQuery getUpgradePointsQuery,
            IInterfaceService interfaceService,
            IInterfaceFormView view,
            DisposeCommand disposeCommand
        )
        {
            _id = id;
            _pauseService = pauseService;
            _interfaceService = interfaceService;
            _view = view;
            _disposeCommand = disposeCommand;
            _coinAmount = coinAmountQuery.Handle();
            _ballsToShoot = getBallsToShootQuery.Handle();
            _upgradePoints = getUpgradePointsQuery.Handle();
        }

        public void Enable()
        {
            _coinAmount.AddListener(OnCoinAmountChanged);
            _ballsToShoot.AddListener(OnBallsToShootChanged);
            _upgradePoints.AddListener(OnUpgradePointsChanged);

            _view.SetMaxUpgradePoints(GameConstants.UpgradeShootingServiceThreshold);
        }

        public void Disable()
        {
            _ballsToShoot.RemoveListener(OnBallsToShootChanged);
            _upgradePoints.RemoveListener(OnUpgradePointsChanged);
            _coinAmount.RemoveListener(OnCoinAmountChanged);
        }

        private void OnUpgradePointsChanged(int upgradeValue) =>
            _view.SetUpgradePoints(upgradeValue);

        private void OnBallsToShootChanged(int amount) =>
            _view.SetBallsToShoot(amount);

        private void OnCoinAmountChanged(int changedValue)
        {
            int difference = changedValue - _currentValue;
            _currentValue = changedValue;

            _view.SetCoinAmount(_currentValue, difference);
        }

        public void OnPauseButtonPressed()
        {
            _interfaceService.Show(new GameplayPauseFormType());
            _pauseService.PauseGame();
        }

        public void OnViewDisposed() =>
            Dispose();

        private void Dispose()
        {
            _disposeCommand.Handle(_id);

            _pauseService = null;
            _interfaceService = null;
            _view = null;
            _disposeCommand = null;
            _coinAmount = null;
            _ballsToShoot = null;
            _upgradePoints = null;
        }
    }
}
