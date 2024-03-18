using Codebase.Core.Common.General.LiveDatas;
using Codebase.Core.Frameworks.EnitySystem.CQRS;
using Codebase.MainMenu.CQRS.Queries;
using Codebase.MainMenu.Presentations.Interfaces;
using Codebase.MainMenu.Services.Interfaces;
using Codebase.MainMenu.Views.Interfaces;
using Codebase.Maps.Common;

namespace Codebase.MainMenu.Presentations.Implementations.LevelsSelectors
{
    public class MainMenuMapPresenter : IMainMenuMapPresenter
    {
        private readonly int _id;
        private readonly IMainMenuMapView _view;
        private readonly ISelectedMapService _selectedMapService;
        private readonly IShopService _shopService;
        private readonly DisposeCommand _disposeCommand;

        private readonly MapType _mapType;
        private readonly int _price;

        private ILiveData<bool> _selectionLiveData;
        private ILiveData<bool> _boughtLiveData;

        public MainMenuMapPresenter
        (
            int id,
            GetMapSelectionQuery getMapSelectionQuery,
            GetMapBoughtQuery getMapBoughtQuery,
            MainMenuMapGetTypeQuery mainMenuMapGetTypeQuery,
            MainMenuMapGetPriceQuery mainMenuMapGetPriceQuery,
            IMainMenuMapView view,
            ISelectedMapService selectedMapService,
            IShopService shopService,
            DisposeCommand disposeCommand
        )
        {
            _id = id;
            _view = view;
            _selectedMapService = selectedMapService;
            _shopService = shopService;
            _disposeCommand = disposeCommand;
            _price = mainMenuMapGetPriceQuery.Handle(id);
            _mapType = mainMenuMapGetTypeQuery.Handle(id);
            _selectionLiveData = getMapSelectionQuery.Handle(id);
            _boughtLiveData = getMapBoughtQuery.Handle(id);
        }

        public void Enable()
        {
            _selectionLiveData.AddListener(OnSelectionChanged);
            _boughtLiveData.AddListener(OnBoughtStateChangedChanged);
            _view.SetMapType(_mapType);
            _view.SetPrice(_price);
        }

        public void Disable()
        {
            _selectionLiveData.RemoveListener(OnSelectionChanged);
            _boughtLiveData.RemoveListener(OnBoughtStateChangedChanged);
        }

        public void OnSelectButtonClick() =>
            _selectedMapService.Select(_id);

        public void OnBuyButtonClick() =>
            _shopService.BuyMap(_id);

        public void OnViewDisposed() =>
            Dispose();

        private void OnBoughtStateChangedChanged(bool isBought) =>
            _view.SetBought(isBought);

        private void OnSelectionChanged(bool value) =>
            _view.SetSelection(value);

        private void Dispose()
        {
            _disposeCommand.Handle(_id);
            Disable();

            _boughtLiveData = null;
            _selectionLiveData = null;
        }
    }
}
