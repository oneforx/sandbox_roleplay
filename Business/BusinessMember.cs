using System;
using System.Collections.Generic;

namespace Roleplay.Business
{

    public class BusinessMember
    {
        public long ClientId;
        public List<Job> ActiveJobs = new();
        public Dictionary<long, JobGrade> ActiveGrades = new();

        public BusinessMember(long clientId, List<Job> activeJobs)
        {
            this.ClientId = clientId;
            this.ActiveJobs = activeJobs;
        }

        public Job? GetActiveJobById(Guid jobId)
        {
            foreach (var job in ActiveJobs)
            {
                if (job.Id == jobId) {
                    return job;
                }
            }

            return null;
        }

        public bool SetActiveJob(Job jobToSet)
        {
            if (this.GetActiveJobById(jobToSet.Id) == null)
            {
                this.ActiveJobs.Add(jobToSet);
                return true;
            }

            return false;
        }

        public bool RemoveActiveJob(Guid jobToRemoveId)
        {
            if (this.GetActiveJobById(jobToRemoveId) != null)
            {
                this.ActiveJobs.RemoveAll((job) => job.Id == jobToRemoveId);
                return true;
            }

            return false;
        }
    }
}