using System.Collections.Generic;

namespace ApplicationCode.Core.Frameworks.EnitySystem.Interfaces
{
    public interface IEntityRepository
    {
        public int Count { get; }
        
        public void Register(IEntity entity);
        public void Remove(IEntity entity);
        public IEntity Get(int id);
        public IEnumerable<IEntity> Get(int[] ids);
    }
}