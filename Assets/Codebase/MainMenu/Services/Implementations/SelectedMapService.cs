using Codebase.MainMenu.CQRS.Commands;
using Codebase.MainMenu.CQRS.Queries;
using Codebase.MainMenu.Services.Interfaces;
using Codebase.Maps.Common;
using Codebase.PlayerData.CQRS.Commands;
using Codebase.PlayerData.CQRS.Queries;

namespace Codebase.MainMenu.Services.Implementations
{
    public class SelectedMapService : ISelectedMapService
    {
        private SetMapSelectionCommand _setMapSelectionCommand;
        private MainMenuMapGetTypeQuery _mainMenuMapGetTypeQuery;
        private SetPlayerSelectedMapCommand _setPlayerSelectedMapCommand;

        private int _currentId = -1;

        public SelectedMapService(SetMapSelectionCommand setMapSelectionCommand, MainMenuMapGetTypeQuery mainMenuMapGetTypeQuery, SetPlayerSelectedMapCommand setPlayerSelectedMapCommand)
        {
            _setMapSelectionCommand = setMapSelectionCommand;
            _mainMenuMapGetTypeQuery = mainMenuMapGetTypeQuery;
            _setPlayerSelectedMapCommand = setPlayerSelectedMapCommand;
        }

        public MapType MapType { get; private set; }
        
        public void Select(int id)
        {
            if (_currentId != -1)
                _setMapSelectionCommand.Handle(_currentId, false);
            
            _currentId = id;
            _setMapSelectionCommand.Handle(_currentId, true);
            MapType = _mainMenuMapGetTypeQuery.Handle(id);
            _setPlayerSelectedMapCommand.Handle(MapType);
        }
    }
}
