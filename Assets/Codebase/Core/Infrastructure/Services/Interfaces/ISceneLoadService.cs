using Cysharp.Threading.Tasks;

namespace Codebase.Core.Infrastructure.Services.Interfaces
{
    public interface ISceneLoadService
    {
        public UniTask LoadSceneAsync(string sceneName);
    }
}
