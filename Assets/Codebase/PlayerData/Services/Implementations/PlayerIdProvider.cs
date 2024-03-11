using Codebase.PlayerData.Services.Interfaces;

namespace Codebase.PlayerData.Services.Implementations
{
    public class PlayerIdProvider : IPlayerIdProvider
    {
        public int Id { get; private set; } = -1;
        
        public void Set(int id)
        {
            if(Id != -1)
                throw new System.Exception("Id already set");
            
            Id = id;
        }
    }
}
