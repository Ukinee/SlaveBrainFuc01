using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Core.Frameworks.MVP.Interfaces;
using Codebase.Forms.Common.FormTypes.MainMenu;
using Codebase.Forms.Presentations.Interfaces.MainMenu;
using Codebase.Forms.Services.Implementations;
using Codebase.Maps.Common;

namespace Codebase.Forms.Presentations.Implementations.MainMenu
{
    public class MainMenuFormPresenter : IMainMenuFormPresenter
    {
        private readonly IInterfaceService _interfaceService;

        public MainMenuFormPresenter(IInterfaceService interfaceService)
        {
            _interfaceService = interfaceService;
        }

        public void Enable()
        {
        }

        public void Disable()
        {
        }

        public void OnClickStart()
        {
        }

        public void OnClickSettings()
        {
            _interfaceService.Show(new MainMenuSettingsFormType());
        }

        public void OnClickLeaderboard()
        {
            _interfaceService.Show(new MainMenuLeaderboardFormType());
        }

        public void OnClickShop()
        {
            _interfaceService.Show(new MainMenuShopFormType());
        }

        public void OnClickMapType(MapType mapType)
        {
            $"{mapType} map type".Log();
        }
    }
}
