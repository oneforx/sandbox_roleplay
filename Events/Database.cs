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

			public const string EmbarkID = "business.client.embark";

			[MethodArguments(typeof(string))]
			public class EmbarkAttribute : EventAttribute
			{
				public EmbarkAttribute() : base(EmbarkID)
				{

				}
			}
		}
	}
}
