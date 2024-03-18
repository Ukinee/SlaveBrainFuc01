using System.Collections.Generic;
using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Forms.Common.FormTypes.MainMenu;
using Codebase.Forms.Services.Implementations;
using Codebase.MainMenu.CQRS.Queries;
using Codebase.MainMenu.Presentations.Interfaces;

namespace Codebase.MainMenu.Presentations.Implementations.Shops
{
    public class MainMenuShopFormPresenter : IMainMenuShopFormPresenter
    {
        private readonly int _id;
        private readonly IInterfaceService _interfaceService;
        private readonly DisposeCommand _disposeCommand;
        
        private ILiveData<IReadOnlyList<int>> _shopLevelsData;

        public MainMenuShopFormPresenter
        (
            int id,
            IInterfaceService interfaceService,
            GetShopLevelIdsQuery getShopLevelIdsQuery,
            DisposeCommand disposeCommand
        )
        {
            _id = id;
            _interfaceService = interfaceService;
            _disposeCommand = disposeCommand;
            _shopLevelsData = getShopLevelIdsQuery.Handle(_id);
        }

        public void Enable()
        {
        }

        public void Disable()
        {
        }

        public void OnViewDisposed()
        {
            Dispose();
        }

        public void OnClickBack()
        {
            _interfaceService.Hide(new MainMenuShopFormType());
        }

        private void Dispose()
        {
            _disposeCommand.Handle(_id);
        }
    }
}
