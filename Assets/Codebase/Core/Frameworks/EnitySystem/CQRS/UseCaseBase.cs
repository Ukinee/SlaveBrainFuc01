using System;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;

namespace Codebase.Core.Frameworks.EnitySystem.CQRS
{
    public class EntityUseCaseBase<T>
    {
        private readonly IEntityRepository _repository;

        public EntityUseCaseBase(IEntityRepository repository)
        {
            _repository = repository;
        }

        protected T Get(int id)
        {
            IEntity entity = _repository.Get(id);
            
            if(entity is not T typedEntity)
                throw new Exception("Type error");
            
            return typedEntity;
        }
        
        protected T2 Get<T2>(int id)
        {
            IEntity entity = _repository.Get(id);
            
            if(entity is not T2 typedEntity)
                throw new Exception("Type error");
            
            return typedEntity;
        }
    }
}
