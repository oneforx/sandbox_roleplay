using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Models
{
    public class LinkJobHasTask
    {
        public Guid JobId { get; set; }
        public Guid TaskId { get; set; }

        public LinkJobHasTask(Guid jobId, Guid taskId)
        {
            JobId = jobId;
            TaskId = taskId;
        }

        public LinkJobHasTask(Job job, Task business)
        {
            JobId = job.Id;
            TaskId = business.Id;
        }
    }
}
