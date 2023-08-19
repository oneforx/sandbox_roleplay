using Sandbox;
using System.Collections.Generic;
using Roleplay.Map;
using Roleplay.Engine;

#nullable enable
namespace Roleplay
{
    public partial class RoleplayGameManager : GameManager
    {
        public static Database RoleplayDatabase { get; private set; }

        public RoleplayGameManager(string mapName) 
        { 
            if(Game.IsServer)
            {
                RoleplayDatabase = Database.Load("backup");
            }
        }

        /*[RoleplayGameEvent.Common.ClientDidAction]
        public static void ListenAction(IAction 
        , IClient client)
        {
            Log.Info(Game.IsServer);
            if (Game.IsClient) return;

            foreach (Business.Business business in LegalBusinessRegistry.Businesses)
            {
                BusinessMember businessMember = business.GetMemberById(client.SteamId);

                if (businessMember == null) return;

                foreach (var activeJob in businessMember.ActiveJobs)
                {

                }
            }

        }*/

        public override void Shutdown()
        {
            if (Game.IsServer)
            {
                RoleplayDatabase.Save();
            }
            base.Shutdown();
        }
    }
}
