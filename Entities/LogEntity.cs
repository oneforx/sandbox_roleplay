using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Sandbox;

namespace Roleplay.Entities
{
	[Spawnable]
	[Library("log", Description = "Log Entity")]
	public partial class LogEntity : Prop, IUse
	{
		public int money { get; set; }

		public override void Spawn()
		{
			SetModel("models/log/log.vmdl");

			base.Spawn();
		}

		public bool OnUse(Entity user)
		{
			Log.Info($"{this} has been used by {user}!");

			this.Delete();

			return false;
		}

		public bool IsUsable(Entity user)
		{
			return true;
		}
	}
}
