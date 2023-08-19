using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roleplay.Engine;

namespace Roleplay.Events
{
    public static class Action
    {
        public static class Common
        {

            public const string ClientDidActionId = "engine.common.did_action";

            [MethodArguments(typeof(Schemas.Action), typeof(IClient))]
            public class ClientDidAction : EventAttribute
            {
                public ClientDidAction() : base(ClientDidActionId)
                {

                }
            }
        }
    }
}
