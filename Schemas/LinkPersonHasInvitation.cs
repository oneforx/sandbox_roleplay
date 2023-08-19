using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Schemas
{
    public class LinkPersonHasInvitation
    {
        public Guid PersonId { get; set; }

        public Guid InvitationId { get; set; }
    }
}
