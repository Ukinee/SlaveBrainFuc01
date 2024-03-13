using Codebase.Game.CQRS.Commands;
using Codebase.Game.Services.Interfaces;

namespace Codebase.Game.Services.Implementations
{
    public class SelectedLevelService : ISelectedLevelService
    {
        private readonly SetLevelSelectionCommand _setLevelSelectionCommand;
        public int CurrentId { get; private set; } = -1;

        public SelectedLevelService(SetLevelSelectionCommand setLevelSelectionCommand)
        {
            _setLevelSelectionCommand = setLevelSelectionCommand;
        }

        public void Select(int id)
        {
            if(CurrentId != -1)
                _setLevelSelectionCommand.Handle(CurrentId, false);
            
            _setLevelSelectionCommand.Handle(id, true);
            CurrentId = id;
        }
    }
}
