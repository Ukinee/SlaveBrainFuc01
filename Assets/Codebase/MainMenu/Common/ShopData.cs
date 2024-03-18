using System;
using System.Collections.Generic;
using Codebase.Maps.Common;
using Unity.Plastic.Newtonsoft.Json;

namespace Codebase.MainMenu.Common
{
    [Serializable]
    public class ShopData
    {
        public ShopData(List<MapShopData> maps, List<StructureShopData> structures)
        {
            Maps = maps;
            Structures = structures;
        }

        [JsonProperty] public List<MapShopData> Maps { get; private set; }
        [JsonProperty] public List<StructureShopData> Structures { get; private set; }
    }

    [Serializable]
    public class MapShopData
    {
        public MapShopData(MapType type, int price)
        {
            Type = type;
            Price = price;
        }

        [JsonProperty] public MapType Type { get; private set; }
        [JsonProperty] public int Price { get; private set; }
    }

    [Serializable]
    public class StructureShopData
    {
        public StructureShopData(string id, int price)
        {
            Id = id;
            Price = price;
        }

        [JsonProperty] public string Id { get; private set; }
        [JsonProperty] public int Price { get; private set; }
    }
}
