using Sandbox;
using System.Collections.Generic;
using Roleplay.Map;
using Roleplay.Engine;

namespace Roleplay
{
    public partial class RoleplayGameManager : GameManager
	{
        public Database Database { get; set; }

		public RoleplayGameManager(string databaseName) : base()
        {   
            if (Game.IsServer)
            {
                Database = Database.Load(databaseName);
            }
            Event.Register(this);
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
