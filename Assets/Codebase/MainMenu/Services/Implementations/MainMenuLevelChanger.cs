using Codebase.App.Infrastructure.StatePayloads;
using Codebase.Core.Infrastructure.StateMachines.Simple;
using Codebase.MainMenu.CQRS.Queries;
using Codebase.MainMenu.Services.Interfaces;

namespace Codebase.MainMenu.Services.Implementations
{
    public class MainMenuLevelChanger : IMainMenuLevelChanger
    {
        private GetLevelIdQuery _getLevelIdQuery;
        private ISelectedLevelService _selectedLevelService;
        private readonly ISelectedMapService _selectedMapService;
        private IStateMachineService<IScenePayload> _sceneStateMachine;

        public MainMenuLevelChanger(GetLevelIdQuery getLevelIdQuery, ISelectedLevelService selectedLevelService, ISelectedMapService selectedMapService, IStateMachineService<IScenePayload> sceneStateMachine)
        {
            _getLevelIdQuery = getLevelIdQuery;
            _selectedLevelService = selectedLevelService;
            _selectedMapService = selectedMapService;
            _sceneStateMachine = sceneStateMachine;
        }

        public void Change()
        {
            if (_selectedLevelService.CurrentId == -1)
                return;

            string levelId = _getLevelIdQuery.Handle(_selectedLevelService.CurrentId);

            _sceneStateMachine.SetState(new GameplayScenePayload(levelId, _selectedMapService.MapType));
        }
    }
}
