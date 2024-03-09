using Codebase.Forms.Common.FormTypes.MainMenu;
using Codebase.Forms.Presentations.Interfaces.MainMenu;
using Codebase.Forms.Services.Implementations;

namespace Codebase.Forms.Presentations.Implementations.MainMenu
{
    public class MainMenuShopFormPresenter : IMainMenuShopFormPresenter
    {
        private readonly IInterfaceService _interfaceService;

        public MainMenuShopFormPresenter(IInterfaceService interfaceService)
        {
            _interfaceService = interfaceService;
        }
        
        public void Enable()
        {
        }

        public void Disable()
        {
        }

        public void OnClickBack()
        {
            _interfaceService.Hide(new MainMenuShopFormType());
        }
    }
}
