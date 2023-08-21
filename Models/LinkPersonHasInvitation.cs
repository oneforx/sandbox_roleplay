using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roleplay.Systems;

namespace Roleplay.Models
{
    public class LinkPersonHasInvitation : Link<Person, Invitation>
    {
        public LinkPersonHasInvitation(Guid personId, Guid invitationId) : base(personId, invitationId)
        {
        }
    }
}
