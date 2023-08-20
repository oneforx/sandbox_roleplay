using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Sandbox;

namespace Roleplay
{
	public partial class DatabaseEntity : Entity
	{

		public Database? Database { get; set; }

		public DatabaseEntity() : base()
		{
			Event.Register(this);
		}

		public override void OnClientActive(IClient client)
		{
			base.OnClientActive(client);
			Log.Info(Game.IsServer);
			// OnClientSetToActive(Database.Serialize());
		}

		[ClientRpc]
		public void OnClientSetToActive(string data)
		{
			Database = Database.Deserialize(data);
			Log.Info("Database");
		}

		[Roleplay.Events.Database.Client.Embark]
		internal void Embarking(string data)
		{
			Log.Info(data);
		}

		public void Shutdown()
		{
			if (Game.IsServer)
			{
				Database.Save();
			}
			this.Delete();
		}
	}
}
