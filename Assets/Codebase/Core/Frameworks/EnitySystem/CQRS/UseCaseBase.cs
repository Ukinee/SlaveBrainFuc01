using System;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;

namespace Codebase.Core.Frameworks.EnitySystem.CQRS
{
    public class EntityUseCaseBase<T>
    {
        private readonly IEntityRepository _repository;

        protected EntityUseCaseBase(IEntityRepository repository)
        {
            _repository = repository;
        }

        protected T Get(int id)
        {
            return Get<T>(id);
        }
        
        protected T1 Get<T1>(int id)
        {
            IEntity entity = _repository.Get(id);
            
            if(entity is not T1 typedEntity)
                throw new Exception("Type error");
            
            return typedEntity;
        }
    }
}
