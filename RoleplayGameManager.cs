using Sandbox;
using System.Collections.Generic;
using Roleplay.Map;
using Roleplay.Engine;

namespace Roleplay
{
    public partial class RoleplayGameManager : GameManager
    {
        public static Roleplay.Database Database { get; private set; }

        public RoleplayGameManager(string mapName) : base()
        { 
            if(Game.IsServer)
            {
				Database = Database.Load("backup");
            }

            

            Event.Register(this);
        }

		public override void OnClientActive(IClient client)
		{
			base.OnClientActive(client);

            OnClientSetToActive(Database.Serialize());
		}

        [ClientRpc]
        public void OnClientSetToActive(string data)
        {
            Database = Database.Deserialize(data);
            Log.Info("Database");
        }

		[Roleplay.Events.Database.Client.Embark]
        public void Embarking(string data)
        {
            Log.Info(data);
        }

        public override void Shutdown()
        {
            if (Game.IsServer)
            {
                Database.Save();
            }
            base.Shutdown();
        }
    }
}
