using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Events
{
    public static class Business
    {
        public static class Server
        {
            public const string PersonJoinedBusinessID = "business.server.client_joined";

			public class PersonJoinedBusiness : EventAttribute 
            {
                public PersonJoinedBusiness() : base(PersonJoinedBusinessID)
                {

                }
			}

			public const string PersonLeftBusinessID = "business.server.client_left";

			public class PersonLeftBusiness : EventAttribute
			{
				public PersonLeftBusiness() : base(PersonLeftBusinessID)
				{

				}
			}
		}

        public static class Client
        {
			public const string OnJoinID = "business.client.on_join";

			[MethodArguments(typeof (Business))]
			public class OnJoin : EventAttribute
			{
				public OnJoin() : base(OnJoinID)
				{

				}
			}


			public const string OnLeftID = "business.client.on_join";

			[MethodArguments(typeof(Models.LeftReason))]
			public class OnLeft : EventAttribute
			{
				public OnLeft() : base(OnLeftID)
				{

				}
			}
		}
    }
}
