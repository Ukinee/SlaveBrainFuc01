using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Core.Services.PauseServices;
using Codebase.Forms.Common.FormTypes.Gameplay;
using Codebase.Forms.Services.Implementations;
using Codebase.Gameplay.Interface.Presentation.Interfaces;
using Codebase.Gameplay.Interface.Views.Interfaces;
using Codebase.Gameplay.PlayerData.CQRS.Queries;

namespace Codebase.Gameplay.Interface.Presentation.Implementations
{
    public class InterfaceFormPresenter : IInterfaceFormPresenter
    {
        private readonly int _id;
        private readonly PauseService _pauseService;
        private readonly IInterfaceService _interfaceService;
        private readonly IInterfaceFormView _view;
        private readonly DisposeCommand _disposeCommand;

        private readonly ILiveData<int> _coinAmount;
        private int _currentValue;

        public InterfaceFormPresenter
        (
            int id,
            PauseService pauseService,
            GetGameplayPlayerCoinAmountQuery coinAmountQuery,
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
        }

        public void Enable()
        {
            _coinAmount.AddListener(OnCoinAmountChanged);
        }

        public void Disable()
        {
        }

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

        public void OnViewDisposed()
        {
            Dispose();
        }

        private void Dispose()
        {
            _disposeCommand.Handle(_id);
        }
    }
}
