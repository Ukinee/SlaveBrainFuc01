using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.MainMenu.CQRS.Queries;
using Codebase.MainMenu.Presentations.Interfaces;
using Codebase.MainMenu.Services.Interfaces;
using Codebase.MainMenu.Views.Interfaces;
using Codebase.Maps.Common;

namespace Codebase.MainMenu.Presentations.Implementations
{
    public class MainMenuMapPresenter : IMainMenuMapPresenter
    {
        private readonly int _id;
        private readonly IMainMenuMapView _view;
        private readonly ISelectedMapService _selectedMapService;
        private readonly MapType _mapType;
        private readonly DisposeCommand _disposeCommand;
        
        private ILiveData<bool> _selectionLiveData;

        public MainMenuMapPresenter
        (
            int id,
            GetMapSelectionQuery getMapSelectionQuery,
            GetMapTypeQuery getMapTypeQuery,
            IMainMenuMapView view,
            ISelectedMapService selectedMapService,
            DisposeCommand disposeCommand
        )
        {
            _id = id;
            _view = view;
            _selectedMapService = selectedMapService;
            _disposeCommand = disposeCommand;
            _mapType = getMapTypeQuery.Handle(id);
            _selectionLiveData = getMapSelectionQuery.Handle(id);
        }

        public void Enable()
        {
            _selectionLiveData.AddListener(OnSelectionChanged);
            _view.SetMapType(_mapType);
        }

        public void Disable()
        {
            _selectionLiveData.RemoveListener(OnSelectionChanged);
        }

        public void OnButtonClick()
        {
            _selectedMapService.Select(_id);
        }

        public void OnViewDisposed()
        {
            Dispose();
        }

        private void OnSelectionChanged(bool value)
        {
            _view.SetSelection(value);
        }

        private void Dispose()
        {
            _disposeCommand.Handle(_id);

            _selectionLiveData = null;
        }
    }
}
