using Codebase.Maps.Common;

namespace Codebase.MainMenu.Services.Interfaces
{
    public interface ISelectedMapService
    {
        public MapType MapType { get; }

        public void Select(int id);
    }
}
