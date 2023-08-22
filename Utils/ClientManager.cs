using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace Roleplay.Utils
{
    public static class ClientManager
    {
        public static IClient? GetClientById(long clientId)
        {
            foreach (var client in Game.Clients)
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
