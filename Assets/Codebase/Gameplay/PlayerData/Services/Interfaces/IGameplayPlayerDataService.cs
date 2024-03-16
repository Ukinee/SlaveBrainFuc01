using System;

namespace Codebase.Gameplay.PlayerData.Services.Interfaces
{
    public interface IGameplayPlayerDataService : IDisposable
    {
        public int Id { get; }
    }
}
