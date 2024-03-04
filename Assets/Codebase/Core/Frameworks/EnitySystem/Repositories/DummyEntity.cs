using System;
using ApplicationCode.Core.Frameworks.EnitySystem.Interfaces;

namespace Assets.Codebase.Core.Frameworks.EnitySystem.Repositories
{
    public class DummyEntity : IEntity
    {
        public int Id { get; set; }
        public event Action<int> Disposed;

        public void Dispose()
        {
        }
        
        public sealed override bool Equals(object obj) =>
            obj is IEntity entity && Id == entity.Id;
        
        public sealed override int GetHashCode() =>
            Id.GetHashCode();

        public int CompareTo(IEntity other) =>
            Id.CompareTo(other.Id);
    }
}