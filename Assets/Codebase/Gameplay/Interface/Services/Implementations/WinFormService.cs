using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Forms.Common.FormTypes.Gameplay;
using Codebase.Forms.Services.Implementations;
using Codebase.Gameplay.Interface.CQRS;
using Codebase.Gameplay.Interface.Services.Interfaces;

namespace Codebase.Gameplay.Interface.Services.Implementations
{
    public class WinFormService : IWinFormService
    {
        private readonly IInterfaceService _interfaceService;
        private readonly SetWinFormCoinAmountCommand _setWinFormCoinAmountCommand;

        public WinFormService(IInterfaceService interfaceService, IEntityRepository entityRepository)
        {
            _interfaceService = interfaceService;
            _setWinFormCoinAmountCommand = new SetWinFormCoinAmountCommand(entityRepository);
        }

        public bool IsContinueClicked { get; private set; }

        public void OnContinueClick() =>
            IsContinueClicked = true;

        public bool GetContinueClicked() =>
            IsContinueClicked;

        public void Enable(int coinAmount)
        {
            _interfaceService.Show(new GameplayWinFormType());

            int formId = _interfaceService.GetId(new GameplayWinFormType());
            _setWinFormCoinAmountCommand.Handle(formId, coinAmount);
        }
    }
}
