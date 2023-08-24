using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roleplay.Models;

namespace Roleplay.Utils
{
    public static partial class Utils
    {

        public static void RunActionBothFromClient(Models.Action action, IClient sourceClient)
        {
            Event.Run(Events.Action.Common.ClientDidActionId, action, sourceClient);
            RunServer(action.Name, sourceClient.SteamId);
        }

        [ConCmd.Server("run_action")]
        public static void RunServer(string actionName, long sourceClientId)
        {
                IClient sourceClient = ClientManager.GetClientById(sourceClientId);
                if (sourceClient != null)
                {
                    //Event.Run(Events.Action.Common.ClientDidActionId, action, sourceClient);
                }
                else
                {
                    Log.Error("Client " + sourceClientId + " could not be found");
                }
        }


        public static void RunActionBothFromServer(Models.Action action, IClient sourceClient)
        {
            Event.Run(Events.Action.Common.ClientDidActionId, action, sourceClient);
            RunClient(action.Name, sourceClient.SteamId);
        }

        [ClientRpc]
        public static void RunClient(string actionName, long sourceClientId)
        {
                IClient sourceClient = ClientManager.GetClientById(sourceClientId);
                if (sourceClient != null)
                {
                   // Event.Run(Events.Action.Common.ClientDidActionId, action, sourceClient);
                }
                else
                {
                    Log.Error("Client " + sourceClientId + " could not be found");
                }
        }
        public static string GetQuery(string queryName, string url = "id=dazdazdza&ownerId=dazdazdzad")
        {
            string queryPrefix = queryName + "=";

            // Si l'URL ne contient pas le nom de la requête, retourner null ou "" selon votre préférence
            if (!url.Contains(queryPrefix))
                return null;

            // Trouver le début du paramètre de requête
            int startIndex = url.IndexOf(queryPrefix) + queryPrefix.Length;

            // Obtenir la fin du paramètre de requête
            int endIndex = url.IndexOf('&', startIndex);
            if (endIndex == -1)
                endIndex = url.Length;

            // Extraire la valeur
            return url.Substring(startIndex, endIndex - startIndex);
        }
    }
}
