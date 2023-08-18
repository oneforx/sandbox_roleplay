using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Engine.Utils
{
    public static class ClientManager
    {
        public static IClient GetClientById(long clientId)
        {
            foreach(var client in Game.Clients)
            {
                if (client.SteamId == clientId)
                {
                    return client;
                }
            }

            return null;
        }
    }
}
