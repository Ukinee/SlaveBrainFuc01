using UnityEngine;

namespace Codebase.Core.Services.Pools
{
    public interface IPool<in T>
    {
        public void ReleaseAll();
        public void Release(T obj);
    }
}
