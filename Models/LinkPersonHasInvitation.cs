using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Models
{
    public class LinkPersonHasInvitation
    {
        public Guid PersonId { get; set; }

        public Guid InvitationId { get; set; }

        public LinkPersonHasInvitation(Guid PersonId, Guid InvitationId)
        {
            this.PersonId = PersonId;
            this.InvitationId = InvitationId;
        }
    }
}
