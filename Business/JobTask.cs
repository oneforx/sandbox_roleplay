using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Business
{
    public class JobTask
    {
        public IList<int> Rewards { get; set; }
        public string Action { get; set; }

        public JobTask(IList<int> rewards, string action)
        {
            this.Rewards = rewards;
            this.Action = action;
        }
    }
}
