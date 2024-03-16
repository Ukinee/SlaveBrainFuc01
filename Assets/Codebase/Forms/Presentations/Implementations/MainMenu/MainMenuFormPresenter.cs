using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.Forms.Common.FormTypes.MainMenu;
using Codebase.Forms.Presentations.Interfaces.MainMenu;
using Codebase.Forms.Services.Implementations;
using Codebase.Maps.Common;

namespace Codebase.Forms.Presentations.Implementations.MainMenu
{
    public class MainMenuFormPresenter : IMainMenuFormPresenter
    {
        private readonly int _id;
        private readonly IInterfaceService _interfaceService;
        private readonly DisposeCommand _disposeCommand;

        public MainMenuFormPresenter(int id, IInterfaceService interfaceService, DisposeCommand disposeCommand)
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

        public void OnClickLevelSelection()
        {
            _interfaceService.Show(new LevelSelectorFormType());
        }

        public void OnClickMapType(MapType mapType)
        {
            $"{mapType} map type".Log();
        }

        public void OnViewDisposed()
        {
            _disposeCommand.Handle(_id);
        }
    }
}
