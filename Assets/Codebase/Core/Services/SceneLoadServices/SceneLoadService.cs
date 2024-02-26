using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Codebase.Core.Services.SceneLoadServices
{
    public class SceneLoadService
    {
        public async UniTask LoadSceneAsync(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
