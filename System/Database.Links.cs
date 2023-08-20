using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roleplay.Models;

namespace Roleplay.System
{
    public partial class Database
    {
        #region Links
        public List<LinkBusinessHasJob> LinkBusinessHasJobs { get; set; } = new List<LinkBusinessHasJob>();

        public List<LinkBusinessHasMember> LinkBusinessHasMembers { get; set; } = new List<LinkBusinessHasMember>();

        public List<LinkJobHasTask> LinkBusinessJobHasTasks { get; set; } = new List<LinkJobHasTask>();

        public List<LinkBusinessMemberHasJob> LinkBusinessMemberHasJobs { get; set; } = new List<LinkBusinessMemberHasJob>();

        #endregion
    }
}
