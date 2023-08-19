using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Schemas
{
    public class LinkPersonHasPhone
    {
        public Guid PersonId { get; set; }

        public Guid PhoneId { get; set; }

    }
}
