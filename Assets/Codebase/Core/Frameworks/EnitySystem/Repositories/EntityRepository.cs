using System.Collections.Generic;
using System.Linq;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;
using Codebase.Core.Common.General.Extensions.ObjectExtensions;
using Codebase.Core.Infrastructure.Repositories.Base;

namespace Assets.Codebase.Core.Frameworks.EnitySystem.Repositories
{
    public class EntityRepository : HashSetRepositoryBase<IEntity>, IEntityRepository
    {
        private readonly DummyEntity _dummyEntity = new DummyEntity();
        
        public IEntity Get(int id)
        {
            _dummyEntity.Id = id;
            return GetInternal(_dummyEntity);
        }

        public IEnumerable<IEntity> Get(int[] ids)
        {
            return ids.Select(Get);
        }

        protected override void OnRegister(IEntity item)
        {
            item.Disposed += OnEntityDisposed;
        }

        protected override void OnRemove(IEntity item)
        {
            item.Disposed -= OnEntityDisposed;
        }

        private void OnEntityDisposed(int id)
        {
            IEntity entity = Get(id);
            
            Remove(entity);
        }
    }
}