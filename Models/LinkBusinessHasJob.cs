using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Models
{
    public class LinkBusinessHasJob : Roleplay.Systems.Link<Business, Job>
    {
        public LinkBusinessHasJob(Guid businessId, Guid jobId) : base(businessId, jobId) { }
    }
}
