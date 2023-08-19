using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay
{
    public partial class Database
    {
        #region Links
        public List<Schemas.LinkBusinessHasJob> LinkBusinessHasJobs { get; set; } = new List<Schemas.LinkBusinessHasJob>();

        public List<Schemas.LinkBusinessHasMember> LinkBusinessHasMembers { get; set; } = new List<Schemas.LinkBusinessHasMember>();

        public List<Schemas.LinkBusinessJobHasTask> LinkBusinessJobHasTasks { get; set; } = new List<Schemas.LinkBusinessJobHasTask>();

        public List<Schemas.LinkBusinessMemberHasJob> LinkBusinessMemberHasJobs { get; set; } = new List<Schemas.LinkBusinessMemberHasJob>();

        #endregion
    }
}
