using Codebase.Game.CQRS.Commands;
using Codebase.Game.Services.Interfaces;

namespace Codebase.Game.Services.Implementations
{
    public class SelectedLevelService : ISelectedLevelService
    {
        private readonly SetLevelSelectionCommand _setLevelSelectionCommand;
        private int _currentId = -1;
        
        public SelectedLevelService(SetLevelSelectionCommand setLevelSelectionCommand)
        {
            _setLevelSelectionCommand = setLevelSelectionCommand;
        }

        public void Select(int id)
        {
            if(_currentId != -1)
                _setLevelSelectionCommand.Handle(_currentId, false);
            
            _setLevelSelectionCommand.Handle(id, true);
            _currentId = id;
        }
    }
}
