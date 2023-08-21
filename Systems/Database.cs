using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Sandbox;

namespace Roleplay.Systems
{
	public partial class Database : BaseNetworkable
    {

        [JsonIgnore]
        public bool ReadyOnClient = false;

        public static Database Current { get; set; }

        public string Name { get; set; }

		public Database(string name)
        {
            Name = name;
            Current = this;
        }


        public static Database Deserialize(string databaseData)
		{
			JsonSerializerOptions options = new();
			options.IncludeFields = true;
			options.WriteIndented = true;
			return JsonSerializer.Deserialize<Database>(databaseData, options);
        }

        public static void Load(string databaseName)
        {
            if (FileSystem.Data.FileExists(databaseName + ".json"))
            {
                Database.Current = Deserialize(FileSystem.Data.ReadAllText(databaseName + ".json"));
                Log.Info(Database.Current.Tables);
            }
            else
            {
                Database newDatabase = new Database(databaseName);
                newDatabase.Save();
                Database.Current = newDatabase;
            }
        }

        public string Serialize()
        {
            JsonSerializerOptions options = new();
            options.IncludeFields = true;
            options.WriteIndented = true;
			return JsonSerializer.Serialize<Database>(this, options);
        }

        public void Save()
        {
            FileSystem.Data.WriteAllText(Name + ".json", this.Serialize());
        }


        [GameEvent.Server.ClientJoined]
        public static void OnClientJoined(ClientJoinedEvent ev)
        {
            CallDatabaseInitEvent(To.Single(ev.Client), Database.Current.Serialize());
        }

        [ClientRpc]
        public static void CallDatabaseInitEvent(string databaseData)
        {
            Event.Run(Events.Database.Client.InitID, Database.Deserialize(databaseData));
        }

        [Events.Database.Client.Init]
        public static void OnClientDatabaseInit(Roleplay.Systems.Database database)
        {
            Database.Current = database;
            Database.Current.ReadyOnClient = true;
        }
    }
}
