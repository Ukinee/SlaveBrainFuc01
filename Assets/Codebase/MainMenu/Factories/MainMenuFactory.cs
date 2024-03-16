using Codebase.Forms.Common.FormTypes.MainMenu;
using Codebase.Forms.Services.Implementations.Factories;

namespace Codebase.MainMenu.Factories
{
    public class MainMenuFactory
    {
        private readonly FormCreationService _formCreationService;

        public MainMenuFactory(FormCreationService formCreationService)
        {
            _formCreationService = formCreationService;
        }

        public void Create()
        {
            _formCreationService.Create(new MainMenuFormType());
            _formCreationService.Create(new LevelSelectorFormType());
            _formCreationService.Create(new MainMenuSettingsFormType());
            _formCreationService.Create(new MainMenuShopFormType());
            _formCreationService.Create(new MainMenuLeaderboardFormType());
        }
    }
}
