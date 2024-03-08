namespace Codebase.App.Infrastructure.StatePayloads
{
    public class InitialScenePayload : IScenePayload
    {
        public string SceneName { get; } = "InitialScene";
    }
}
