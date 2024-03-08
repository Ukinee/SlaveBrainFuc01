using System;
using Unity.Plastic.Newtonsoft.Json;

namespace Codebase.Game.Data.Common
{
    [Serializable]
    public class GamePresets
    {
        public GamePresets(GamePresetData[] gamePresetDatas )
        {
            GamePresetDatas = gamePresetDatas;
        }

        public GamePresetData[] GamePresetDatas;
    }
}
