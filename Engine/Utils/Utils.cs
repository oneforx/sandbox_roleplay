using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Engine.Utils
{
    public static class Utils
    {
        public static void RunActionBoth(IAction action, IClient sourceClient)
        {
            Event.Run(Roleplay.Events.Action.Common.ClientDidActionId, action, sourceClient);
            RunServer(action.Name, sourceClient.SteamId);
        }

        [ConCmd.Server("run_action")]
        public static void RunServer(string actionName, long sourceClientId)
        {
            if (Actions.Library.TryGetValue(actionName, out IAction action))
            {
                IClient sourceClient = ClientManager.GetClientById(sourceClientId);
                if (sourceClient != null)
                {
                    Event.Run(Roleplay.Events.Action.Common.ClientDidActionId, action, sourceClient);
                }
                else
                {
                    Log.Error("Client " + sourceClientId + " could not be found");
                }
            }
            else
            {
                Log.Error("Action " + actionName + " not register. Use Actions.Register(IAction)");
            }
        }
    }
}
