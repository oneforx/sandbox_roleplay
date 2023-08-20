using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roleplay.Engine;
using Roleplay.Models;

namespace Roleplay.Events
{
    public static class Action
    {
        public static class Common
        {

            public const string ClientDidActionId = "engine.common.did_action";

            [MethodArguments(typeof(Models.Action), typeof(IClient))]
            public class ClientDidAction : EventAttribute
            {
                public ClientDidAction() : base(ClientDidActionId)
                {

                }
            }
        }
    }
}
