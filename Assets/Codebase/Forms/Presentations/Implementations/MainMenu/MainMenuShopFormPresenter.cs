using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Forms.Common.FormTypes.MainMenu;
using Codebase.Forms.Presentations.Interfaces.MainMenu;
using Codebase.Forms.Services.Implementations;

namespace Codebase.Forms.Presentations.Implementations.MainMenu
{
    public class MainMenuShopFormPresenter : IMainMenuShopFormPresenter
    {
        private readonly int _id;
        private readonly IInterfaceService _interfaceService;
        private readonly DisposeCommand _disposeCommand;

        public MainMenuShopFormPresenter(int id, IInterfaceService interfaceService, DisposeCommand disposeCommand)
        {
            _id = id;
            _interfaceService = interfaceService;
            _disposeCommand = disposeCommand;
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
