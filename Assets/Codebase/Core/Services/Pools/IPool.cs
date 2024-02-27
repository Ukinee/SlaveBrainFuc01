using UnityEngine;

namespace Codebase.Core.Services.Pools
{
    public interface IPool<in T>  where T : MonoBehaviour
    {
        public void ReleaseAll();
        public void Release(T obj);
    }
}
