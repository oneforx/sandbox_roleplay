using Roleplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Systems
{
    public partial class Database
	{
		public Invitation CreateInvitation(Invitation invitation)
		{
			this.Tables[invitation.Id] = invitation;
			return (Invitation)this.Tables[invitation.Id];
		}

		public Link<Person, Invitation> LinkPersonToInvitation(Guid personId, Guid invitationId)
		{
			Link <Person, Invitation> linkPersonHasInvitation = new Link<Person, Invitation>(personId, invitationId);
			this.Tables.Add(linkPersonHasInvitation.Id, linkPersonHasInvitation);
			return linkPersonHasInvitation;
		}
		public bool LinkMultiplePersonToInvitation(Guid[] personIds, Guid invitationId)
		{
			foreach (var personId in personIds)
			{
				this.LinkPersonToInvitation(personId, invitationId);
			}

			return true;
		}
	}
}
