using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Business
{
    public class BusinessMemberInvitation
    {
        public long ClientId { get; set; }
        public Job Job { get; set; } = JobArchetypeLibrary.ButcherJob;

        public BusinessMemberInvitation(long clientId, Job job)
        {
            ClientId = clientId;
            Job = job;
        }
    }
}
