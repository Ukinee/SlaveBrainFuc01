using Codebase.Core.Services.ArrayCrawler;
using Codebase.Cubes.Models;
using Codebase.Structures.Models;
using Codebase.Structures.Services.Interfaces;

namespace Codebase.Structures.Services.Implementations
{
    public class StructureService : IStructureService
    {
        private ArrayCrawler<CubeModel> _crawler = new ArrayCrawler<CubeModel>();
        private FragmentationService _fragmentationService;
        private StructureModel _structureModel;
        private CubeModel[,] _cubes;

        public StructureService
            (FragmentationService fragmentationService, StructureModel structureModel, int width, int height)
        {
            _structureModel = structureModel;
            _fragmentationService = fragmentationService;
            _cubes = new CubeModel[height, width];
        }

        public void Add(CubeModel model, int y, int x)
        {
            if (model.IsActivated)
                return;

            model.Activated += Remove;
            _cubes[y, x] = model;
            _structureModel.Add();
        }

        public void Remove(CubeModel model)
        {
            model.Activated -= Remove;

            if (SetNull(model) == false)
                return;

            _structureModel.Remove();

            if (_structureModel.Amount == 0)
            {
                Dispose();

                return;
            }

            CubeModel[][,] islands = _crawler.Crawl(_cubes);

            if (islands.Length == 1)
                return;

            _fragmentationService.Handle(islands);
            Dispose();
        }

        private bool SetNull(CubeModel model)
        {
            for (int y = 0; y < _cubes.GetLength(0); y++)
            for (int x = 0; x < _cubes.GetLength(1); x++)
            {
                if (_cubes[y, x] != model)
                    continue;

                _cubes[y, x] = null;

                return true;
            }

            return false;
        }

        private void Dispose()
        {
            foreach (CubeModel cube in _cubes)
                if (cube != null)
                    cube.Activated -= Remove;

            _structureModel.Dispose();
            _structureModel = null;
            _cubes = null;
        }
    }
}
