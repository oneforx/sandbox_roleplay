using Roleplay.Models;
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
            public const string PersonJoinedBusinessID = "business.server.person_joined";

			[MethodArguments(typeof (Person), typeof(Models.Business))]
			public class PersonJoinedBusiness : EventAttribute 
            {
                public PersonJoinedBusiness() : base(PersonJoinedBusinessID) {}
			}


			public const string PersonCreatedBusinessID = "business.server.person_created_business";

			[MethodArguments(typeof(Person), typeof(Models.Business))]
			public class PersonCreatedBusiness : EventAttribute
			{
				public PersonCreatedBusiness() : base(PersonCreatedBusinessID) {}
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
			public const string OnSelfJoinID = "business.client.on_join";


			/// <summary>
			/// Quand le client rejoint un business
			/// <example>
			/// <code>
			/// [Roleplay.Events.Business.Client.SelfJoin]
			/// public void OnSelfJoin(Models.Business business) {}
			/// </code>
			/// </example>
			/// </summary>
			[MethodArguments(typeof (Business))]
			public class SelfJoin : EventAttribute
			{
				public SelfJoin() : base(OnSelfJoinID)
				{

				}
			}


			public const string OnSelfLeftID = "business.client.on_join";

			/// <summary>
			/// Quand le client quitte ou se fais bannir d'un business
			/// <example>
			/// <code>
			/// [Roleplay.Events.Business.Client.Left]
			/// public void OnLeft(Models.LeftReason reason) {}
			/// </code>
			/// </example>
			/// </summary>
			[MethodArguments(typeof(Models.LeftReason))]
			public class Left : EventAttribute
			{
				
				public Left() : base(OnSelfLeftID)
				{

				}
			}
		}
    }
}
