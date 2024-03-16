using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Gameplay.Interface.CQRS.Queries;
using Codebase.Gameplay.Interface.Presentation.Interfaces;
using Codebase.Gameplay.Interface.Services.Interfaces;
using Codebase.Gameplay.Interface.Views.Interfaces;

namespace Codebase.Gameplay.Interface.Presentation.Implementations
{
    public class GameplayWinFormPresenter : IWinFormPresenter
    {
        private readonly int _id;
        private readonly IWinFormView _winFormView;
        private readonly IWinFormService _winFormService;
        private readonly DisposeCommand _disposeCommand;

        private readonly ILiveData<int> _coinAmount;

        public GameplayWinFormPresenter
        (
            int id,
            GetWinFormCoinAmountQuery getWinFormCoinAmountQuery,
            IWinFormView winFormView,
            IWinFormService winFormService,
            DisposeCommand disposeCommand
        )
        {
            _id = id;
            _winFormView = winFormView;
            _winFormService = winFormService;
            _disposeCommand = disposeCommand;
            _coinAmount = getWinFormCoinAmountQuery.Handle(_id);
        }

        public void Enable()
        {
            _coinAmount.AddListener(OnCoinsAmountChanged);
        }

        public void Disable()
        {
            _coinAmount.RemoveListener(OnCoinsAmountChanged);
        }

        private void OnCoinsAmountChanged(int amount) =>
            _winFormView.SetCoinAmount(amount);

        public void OnContinueClick() =>
            _winFormService.OnContinueClick();

        public void OnViewDisposed() =>
            Dispose();

        private void Dispose() =>
            _disposeCommand.Handle(_id);
    }
}
