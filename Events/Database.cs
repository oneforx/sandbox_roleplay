using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Roleplay.Events.Business.Client;

namespace Roleplay.Events
{
	public static class Database
	{
		public static class Client
		{

			public const string InitID = "business.client.init";

			[MethodArguments(typeof(Roleplay.Systems.Database))]
			public class InitAttribute : EventAttribute
			{
				public InitAttribute() : base(InitID)
				{

				}
			}


			public const string OnTableUpdatedID = "business.client.table_updated";

			[MethodArguments(typeof(Database))]
			public class OnTableUpdatedAttribute : EventAttribute
			{
				public OnTableUpdatedAttribute() : base(OnTableUpdatedID)
				{

				}
			}
		}
	}
}
