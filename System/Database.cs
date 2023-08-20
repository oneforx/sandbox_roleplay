using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Sandbox;

namespace Roleplay.System
{
	public partial class Database
    {

        public static Database Current { get; set; }

        public string Name { get; set; }

		public Database(string name)
        {
            Name = name;
            Current = this;
        }



        public static Database Deserialize(string databaseData)
        {
            return JsonSerializer.Deserialize<Database>(databaseData, new JsonSerializerOptions { IncludeFields = true, WriteIndented = true });
        }

        public static void Load(string databaseName)
        {
            Database.Current = Deserialize(FileSystem.Data.ReadAllText(databaseName + ".json"));
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize<Database>(this, new JsonSerializerOptions { IncludeFields = true, WriteIndented = true });
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
        public static void OnClientDatabaseInit(Roleplay.System.Database database)
        {
            Database.Current = database;
        }
    }
}
