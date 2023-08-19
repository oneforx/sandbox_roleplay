using Roleplay.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Roleplay.Schemas;
using Sandbox;

namespace Roleplay
{
    public partial class Database
    {
        public string Name { get; set; }

        #region Objects

        public Dictionary<Guid, Schemas.Invitation> Invitations { get; set; } = new Dictionary<Guid, Schemas.Invitation>();
        #endregion

        

        public Database(string name)
        {
            Name = name;
        }

        public static Database Deserialize(string databaseData)
        {
            return JsonSerializer.Deserialize<Database>(databaseData, new JsonSerializerOptions { IncludeFields = true, WriteIndented = true });
        }

        public static Database Load(string databaseName)
        {
            if (FileSystem.Data.FileExists(databaseName + ".json"))
            {

                return Deserialize(FileSystem.Data.ReadAllText(databaseName + ".json"));
            }
            else
            {
                Database businessRegistry = new(databaseName);

                businessRegistry.Save();

                return businessRegistry;
            }
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize<Database>(this, new JsonSerializerOptions { IncludeFields = true, WriteIndented = true });
        }

        public void Save()
        {
            FileSystem.Data.WriteAllText(Name + ".json", this.Serialize());
        }
    }
}
