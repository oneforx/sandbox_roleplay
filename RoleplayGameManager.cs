using Sandbox;
using System.Collections.Generic;
using Roleplay.Engine;
using Roleplay.Systems;

namespace Roleplay
{
    public partial class RoleplayGameManager : GameManager
	{
		public RoleplayGameManager(string databaseName) : base()
        {   
            if (Game.IsServer)
            {
                Database.Load(databaseName);
            }
            else
            {
                Database.Current = new Database(databaseName);
            }
            Event.Register(this);
        }


        public override void Shutdown()
        {
            if (Game.IsServer)
            {
                Database.Current.Save();
            }
            base.Shutdown();
        }
    }
}
