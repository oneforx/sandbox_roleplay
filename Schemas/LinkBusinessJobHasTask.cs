using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Schemas
{
    public class LinkBusinessJobHasTask
    {
        public Guid JobId { get; set; }
        public Guid TaskId { get; set; }
    }
}
