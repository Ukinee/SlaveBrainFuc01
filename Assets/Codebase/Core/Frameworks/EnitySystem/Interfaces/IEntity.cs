using System;

namespace ApplicationCode.Core.Frameworks.EnitySystem.Interfaces
{
    public interface IEntity : IDisposable, IComparable<IEntity>
    {
        public int Id { get; }
        
        public event Action<int> Disposed;
    }
}