using Roleplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay
{
    public partial class Database
	{
		public Dictionary<Guid, Invitation> Invitations { get; set; } = new Dictionary<Guid, Invitation>();


		public Invitation CreateInvitation(Invitation invitation)
		{
			this.Invitations[invitation.Id] = invitation;
			return this.Invitations[invitation.Id];
		}

		public LinkPersonHasInvitation LinkPersonToInvitation(Guid personId, Guid invitationId)
		{
			LinkPersonHasInvitation linkPersonHasInvitation = new LinkPersonHasInvitation(personId, invitationId);
			this.LinkPersonHasInvitations.Add(linkPersonHasInvitation);
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
