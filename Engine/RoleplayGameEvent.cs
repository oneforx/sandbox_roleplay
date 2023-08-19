using Roleplay.Business;
using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Engine
{
    public interface IRoleplayGameEvent
    {
        string Name { get; }

        public void On();

        public void Bind(object? instance);
    }
    public static class RoleplayGameEvent
    {

        public static class Common
        {
            public sealed class ClientJoinedBusinessEvent
            {
                //
                // Résumé :
                //     The client that has joined.
                public IClient Client { get; init; }
                public Business.Business Business { get; init; }

                internal ClientJoinedBusinessEvent(IClient client, Business.Business business)
                {
                    Client = client;
                    Business = business;
                }
            }

            [MethodArguments(new Type[] { typeof(ClientJoinedBusinessEvent) })]
            public class ClientJoinedBusiness : EventAttribute
            {
                public ClientJoinedBusiness() : base("business.common.joined")
                {
                }
            }

        }
    }
}
