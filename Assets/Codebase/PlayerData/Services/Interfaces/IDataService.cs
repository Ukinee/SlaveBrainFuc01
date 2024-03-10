using Codebase.PlayerData.Infrastructure.DTO;

namespace Codebase.PlayerData.Services.Interfaces
{
    public interface IDataService
    {
        public PlayerDataObject Get();
        public void Save();
        public void Clear();
    }
}
