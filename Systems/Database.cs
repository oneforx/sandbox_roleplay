using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Sandbox;
using Roleplay.Models;

namespace Roleplay.Systems
{
    public partial class Database : BaseNetworkable
    {

        [JsonIgnore]
        public bool ReadyOnClient = false;


        public static Database Current { get; set; }

        public static Dictionary<string, Type> Types = new()
        {
            { "Action", typeof(Models.Action) },
            { "Bank", typeof(Bank) },
            { "BankAccount", typeof(BankAccount) },
            { "Business", typeof(Business) },
            { "Invitation", typeof(Invitation) },
            { "Job", typeof(Job) },
            { "Map", typeof(Map) },
            { "MapProperty", typeof(MapProperty) },
            { "Person", typeof(Person) },
            { "Link", typeof(Link) },
            { "Task", typeof(Models.Task) },
		};

        public static Dictionary<string, Type> LinkTypes = new()
        {
            { "PersonToBusiness", typeof(Link<Person, Business>) },
            { "BusinessToPerson", typeof(Link<Business, Person>) },
			{ "BusinessToJob", typeof(Link<Business, Job>) },
            { "BusinessToTask", typeof(Link<Business, Models.Task>) },
            { "JobToTask", typeof(Link<Job, Models.Task>) },
            { "JobToPerson", typeof(Link<Job, Models.Person>) },
            { "PersonToBankAccount", typeof(Link<Person, BankAccount>) },
        };


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
            options.Converters.Add(new TableConverter());
            return JsonSerializer.Deserialize<Database>(databaseData, options);
        }

        public static void Load(string databaseName)
        {
            if (FileSystem.Data.FileExists(databaseName + ".json"))
            {
                Database.Current = Deserialize(FileSystem.Data.ReadAllText(databaseName + ".json"));
            }
            else
            {
                Database newDatabase = new Database(databaseName);
                newDatabase.Save();
                Database.Current = newDatabase;
            }
        }

        public static void RegisterTableType(Type type)
        {
            throw new Exception("The table type" + typeof(Type).Name);
        }

        public static void RegisterTableType<T>()
        {
            throw new Exception("The table type" + typeof(T).Name);
        }

        public string Serialize()
        {
            JsonSerializerOptions options = new();
            options.IncludeFields = true;
            options.WriteIndented = true;
            options.Converters.Add(new TableConverter());
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

        [ClientRpc]
        public static void AddTableOnClient(string rawTable)
		{
            Log.Info("AddTableOnClient" + Game.LocalClient.SteamId + ":\n" + rawTable);
			JsonSerializerOptions options = new();
			options.IncludeFields = true;
			options.WriteIndented = true;
			options.Converters.Add(new TableConverter());
			Table table = JsonSerializer.Deserialize<Table>(rawTable, options);
            if (Database.Current.Tables.ContainsKey(table.Id))
            {
                throw new Exception("The table " + table.Id.ToString() + " of type " + table.TableType + " already exist");
            }
			Database.Current.Tables[table.Id] = table;
		}

        [ClientRpc]
        public static void UpdateTableOnClient(string rawTable)
		{
			Log.Info("AddTableOnClient" + Game.LocalClient.SteamId + ":\n" + rawTable);
			JsonSerializerOptions options = new();
			options.IncludeFields = true;
			options.WriteIndented = true;
			options.Converters.Add(new TableConverter());
			Table table = JsonSerializer.Deserialize<Table>(rawTable, options);
			Database.Current.Tables[table.Id] = table;
		}

        [ClientRpc]
        public static void DeleteTableOnClient(string rawTableId)
        {
			Guid tableId = Guid.Parse(rawTableId);

			if (Database.Current.Tables.ContainsKey(tableId))
			{
				Database.Current.Tables.Remove(tableId);
			}
			else
			{
				throw new Exception("Trying to delete the table + " + tableId.ToString() + " but it doesn't exist on the client.");
			}
		}

        [Events.Database.Client.Init]
        public static void OnClientDatabaseInit(Roleplay.Systems.Database database)
        {
            Database.Current = database;
            Database.Current.ReadyOnClient = true;
        }

		[Events.Database.Client.OnTableAdded]
		public static void OnTableAdded(Table table)
		{

		}

		[Events.Database.Client.OnTableUpdated]
		public static void OnTableUpdated(Table table)
		{

		}


		[Events.Database.Client.OnTableDeleted]
        public static void OnTableDeleted(Table table)
        {
		}
	}
}
