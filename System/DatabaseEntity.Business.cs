using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roleplay.Events;
using Roleplay.Models;
using Sandbox;

namespace Roleplay
{
	// DatabaseEntity.Business

	public partial class DatabaseEntity : Entity
	{
		public void PersonJoinedBusiness(Models.Business business, LinkPersonHasBusiness linkPersonHasBusiness)
		{
			Person? person = this.Database.GetPersonById(linkPersonHasBusiness.PersonId);
			if (person != null)
			{
				Event.Run(Events.Business.Server.PersonJoinedBusinessID, person, business);
			}
		}

		public void PersonCreatedBusiness(Models.Business business, LinkPersonHasBusiness linkPersonHasBusiness)
		{
			Person? person = this.Database.GetPersonById(linkPersonHasBusiness.PersonId);
			if (person != null)
			{
				Event.Run(Events.Business.Server.PersonCreatedBusinessID, person, business);
			}
		}
	}
}
