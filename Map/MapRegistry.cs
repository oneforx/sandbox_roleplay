using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Sandbox;

namespace Roleplay.Map
{
    public class MapRegistry
    {
        public string Name { get; set; }
        public static List<Region> Regions { get; set; }
        public static List<Property> Property { get; set; }

        public MapRegistry(string name)
        {
            Name = name;
        }

        public Region GetRegionById(Guid id)
        {
            foreach (Region region in Regions)
            {
                if (region.Id == id)
                {
                    return region;
                }
            }

            return null;
        }

        public static MapRegistry Deserialize(string mapRegistryData)
        {
            return JsonSerializer.Deserialize<MapRegistry>(mapRegistryData, new JsonSerializerOptions { IncludeFields = true, WriteIndented = true });
        }

        public static MapRegistry Load(string mapName)
        {
            if (FileSystem.Data.FileExists(mapName + ".json"))
            {
                return Deserialize(FileSystem.Data.ReadAllText(mapName + ".json"));
            }
            else
            {
                MapRegistry mapRegistry = new(mapName);

                mapRegistry.Save();

                return mapRegistry;
            }
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize<MapRegistry>(this, new JsonSerializerOptions { IncludeFields = true, WriteIndented = true });
        }

        public void Save()
        {
            FileSystem.Data.WriteAllText(Name + ".json", this.Serialize());
        }
    }
}
