using System;
using System.Linq;
using Codebase.Core.Frameworks.EnitySystem.General;
using Codebase.Core.Services.ArrayCrawler;
using Codebase.Cubes.Models;

namespace Codebase.Structures.Models
{
    public class StructureModel : BaseEntity
    {
        private ArrayCrawler<int> _crawler = new ArrayCrawler<int>();
        
        private readonly int[,] _cubes;

        public StructureModel(int id, int width, int height) : base(id)
        {
            _cubes = new int[height, width];
        }
        
        public bool IsEmpty => Contains() == false;

        public event Action<int[][,]> Fragmented;

        public void Set(int cubeId, int y, int x)
        {
            _cubes[y, x] = cubeId;
        }

        public bool TryGetIndexers(int cubeId, out int height, out int width)
        {
            height = -1;
            width = -1;
            
            for (int y = 0; y < _cubes.GetLength(0); y++)
            for (int x = 0; x < _cubes.GetLength(1); x++)
            {
                if (_cubes[y, x] != cubeId)
                    continue;

                height = y;
                width = x;
                
                return true;
            }

            return false;
        }

        public void HandleFragmentation()
        {
            int[][,] islands = _crawler.Crawl(_cubes);

            if (islands.Length == 1)
                return;

            Fragmented?.Invoke(islands);
            Dispose();
        }

        private bool Contains()
        {
            return _cubes.Cast<int>().Any(id => id != 0);
        }
    }
}
