using Roleplay.Systems;
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


			public const string OnTableAddedID = "business.client.table_added";

			[MethodArguments(typeof(Table))]
			public class OnTableAdded : EventAttribute
			{
				public OnTableAdded() : base(OnTableAddedID)
				{

				}
			}


			public const string OnTableUpdatedID = "business.client.table_updated";

			[MethodArguments(typeof(Table))]
			public class OnTableUpdated : EventAttribute
			{
				public OnTableUpdated() : base(OnTableUpdatedID)
				{

				}
			}


			public const string OnTableDeletedID = "business.client.table_deleted";

			[MethodArguments(typeof(Table))]
			public class OnTableDeleted : EventAttribute
			{
				public OnTableDeleted() : base(OnTableDeletedID)
				{

				}
			}
		}
	}
}
