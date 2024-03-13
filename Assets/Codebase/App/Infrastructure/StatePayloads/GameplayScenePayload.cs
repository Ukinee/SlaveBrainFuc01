using Codebase.Maps.Common;

namespace Codebase.App.Infrastructure.StatePayloads
{
    public class GameplayScenePayload : IScenePayload
    {
        public GameplayScenePayload(string levelId, MapType mapType)
        {
            LevelId = levelId;
            MapType = mapType;
        }

        public string SceneName => "GameplayScene";
        public string LevelId { get; }
        public MapType MapType { get; }
    }
}
