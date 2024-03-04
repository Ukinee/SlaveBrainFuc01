using System;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;

namespace Codebase.Core.Frameworks.EnitySystem.General
{
    public abstract class BaseEntity : IEntity
    {
        protected BaseEntity(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
        public event Action<int> Disposed;

        public void Dispose()
        {
            OnDispose();
            Disposed?.Invoke(Id);
            Id = -1;
        }

        public sealed override bool Equals(object obj) =>
            obj is IEntity entity && Id == entity.Id;
        
        public sealed override int GetHashCode() =>
            Id.GetHashCode();

        public int CompareTo(IEntity other) =>
            Id.CompareTo(other.Id);

        protected virtual void OnDispose()
        {
            
        }
    }
}
