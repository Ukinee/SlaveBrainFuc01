using UnityEngine;

namespace Codebase.Core.Services.Pools
{
    public interface IPool<in T>
    {
        public void Release(T obj);
    }
}
