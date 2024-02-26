using Codebase.Core.Infrastructure.Services.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Codebase.Core.Infrastructure.Services.Implementations
{
    public class SceneLoadService : ISceneLoadService
    {
        public async UniTask LoadSceneAsync(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
