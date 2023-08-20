using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Models
{
    public class LinkBusinessMemberHasJob
    {
        public Guid BusinessId { get; set; }

        public Guid MemberId { get; set; }

        public Guid JobId { get; set; }
    }
}
