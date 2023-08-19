using Roleplay.Business;
using Roleplay.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable
namespace Roleplay
{
    public partial class Database
    {
        public Dictionary<Guid, Schemas.Business> Businesses { get; set; } = new Dictionary<Guid, Schemas.Business>();

        public Dictionary<Guid, Schemas.BusinessJob> BusinessJobs { get; set; } = new Dictionary<Guid, Schemas.BusinessJob>();

        public Dictionary<Guid, Schemas.BusinessMember> BusinessMembers { get; set; } = new Dictionary<Guid, Schemas.BusinessMember>();

        public Dictionary<Guid, Schemas.BusinessTask> BusinessTasks { get; set; } = new Dictionary<Guid, Schemas.BusinessTask>();

        #region [CRUD] Business
        
        
        public Schemas.Business CreateBusiness(Schemas.Business business)
        {
            if (business == null)
            {
                business = new Schemas.Business(Guid.NewGuid().ToString());
                this.Businesses[business.Id] = business;
                return this.Businesses[business.Id];
            }

            this.Businesses[business.Id] = business;
            return this.Businesses[business.Id];
        }

        public Schemas.Business? GetBusinessById(Guid businessId)
        {
            return this.Businesses[businessId];
        }

        public Dictionary<Guid, Schemas.Business> GetAllBusiness()
        {
            return this.Businesses;
        }

        public Dictionary<Guid, Schemas.Business> GetAllBusinessByPersonId(Guid personId)
        {
            Dictionary<Guid, Schemas.Business> businesses = new();

            foreach (Schemas.LinkPersonHasBusiness linkPersonHasBusiness in this.LinkPersonHasBusinesses)
            {
                if (linkPersonHasBusiness.PersonId == personId && this.Businesses.ContainsKey(linkPersonHasBusiness.BusinessId))
                {
                    businesses[linkPersonHasBusiness.BusinessId] = this.Businesses[linkPersonHasBusiness.BusinessId];
                }
            }

            return businesses;
        }

        public bool SoftDeleteBusinessById(Guid businessId)
        {
            Schemas.Business? business = GetBusinessById(businessId);
            if (business != null)
            {
                business.SoftDelete();
                return true;
            }
            return false;
        }

        public bool DeleteBusinessById(Guid businessId)
        {
            if (!this.Businesses.ContainsKey(businessId)) return false;

            this.Businesses.Remove(businessId);

            // Remove all links

            this.LinkBusinessHasJobs.ForEach((linkBusinessToJob) => DeleteBusinessJobById(linkBusinessToJob.JobId));
                
            this.LinkBusinessHasJobs.RemoveAll((linkJob) => linkJob.BusinessId == businessId);

            return true;
        }

        #endregion

        #region [CRUD] BusinessJob

        public Schemas.BusinessJob? GetBusinessJobById(Guid jobId)
        {
            return this.BusinessJobs[jobId];
        }

        public Dictionary<Guid, Schemas.BusinessJob> GetAllBusinessJobs()
        {
            return this.BusinessJobs;
        }

        public Dictionary<Guid, Schemas.BusinessJob> GetAllBusinessJobsByBusinessId(Guid businessId)
        {
            Dictionary<Guid, Schemas.BusinessJob> businessJobs = new();

            foreach (Schemas.LinkBusinessHasJob linkBusinessHasJob in this.LinkBusinessHasJobs)
            {
                if (linkBusinessHasJob.BusinessId == businessId && this.BusinessJobs.ContainsKey(linkBusinessHasJob.JobId))
                {
                    businessJobs[linkBusinessHasJob.JobId] = this.BusinessJobs[linkBusinessHasJob.JobId];
                }
            }

            return businessJobs;
        }
    
        public bool DeleteBusinessJobById(Guid jobId)
        {
            if (!this.BusinessJobs.ContainsKey(jobId)) return false;

            this.BusinessJobs.Remove(jobId);

            this.LinkBusinessJobHasTasks.ForEach((linkBusinessJobToTask) => DeleteBusinessTaskById(linkBusinessJobToTask.TaskId));

            this.LinkBusinessJobHasTasks.RemoveAll((linkBusinessJobToTask) => linkBusinessJobToTask.JobId == jobId);

            return true;
        }
        
        #endregion

        #region [CRUD] BusinessTask

        public Schemas.BusinessTask? GetBusinessTaskById(Guid taskId)
        {
            return this.BusinessTasks[taskId];
        }

        public Dictionary<Guid, Schemas.BusinessTask> GetAllBusinessTasks()
        {
            return this.BusinessTasks;
        }

        public Dictionary<Guid, Schemas.BusinessTask> GetAllBusinessTasksByBusinessJobId(Guid jobId)
        {
            Dictionary<Guid, Schemas.BusinessTask> businessTasks = new();

            foreach (Schemas.LinkBusinessJobHasTask linkBusinessJobHasTask in this.LinkBusinessJobHasTasks)
            {
                if (linkBusinessJobHasTask.JobId == jobId && this.BusinessTasks.ContainsKey(linkBusinessJobHasTask.TaskId))
                {
                    businessTasks[linkBusinessJobHasTask.TaskId] = this.BusinessTasks[linkBusinessJobHasTask.TaskId];
                }
            }

            return businessTasks;
        }

        // Get all BusinessTask of a Business
        public Dictionary<Guid, Schemas.BusinessTask> GetAllBusinessTasksByBusinessId(Guid businessId)
        {
            Dictionary<Guid, Schemas.BusinessTask> businessTasks = new Dictionary<Guid, Schemas.BusinessTask>();

            foreach (var linkBusinessJobToTask in this.LinkBusinessJobHasTasks)
            {
                var linkedJob = this.LinkBusinessHasJobs.Find(linkBusinessToJob =>
                    linkBusinessToJob.BusinessId == businessId && linkBusinessToJob.JobId == linkBusinessJobToTask.JobId);

                if (linkedJob != null && this.BusinessTasks.ContainsKey(linkBusinessJobToTask.TaskId))
                {
                    businessTasks[linkBusinessJobToTask.TaskId] = this.BusinessTasks[linkBusinessJobToTask.TaskId];
                }
            }

            return businessTasks;
        }


        public bool DeleteBusinessTaskById(Guid taskId)
        {
            if (!this.BusinessTasks.ContainsKey(taskId)) return false;

            this.BusinessTasks.Remove(taskId);

            return true;
        }

        #endregion

        #region [CRUD] BusinessMember

        public Schemas.BusinessMember CreateBusinessMember(Schemas.BusinessMember businessMember, Schemas.Business business)
        {
            this.BusinessMembers[businessMember.Id] = businessMember;

            this.LinkBusinessHasMembers.Add(new LinkBusinessHasMember(businessMember, business));

            return this.BusinessMembers[businessMember.Id];
        }

        public Schemas.BusinessMember? GetBusinessMemberById(Guid taskId)
        {
            return this.BusinessMembers[taskId];
        }

        public Dictionary<Guid, Schemas.BusinessMember> GetAllBusinessMembers()
        {
            return this.BusinessMembers;
        }

        public List<Schemas.BusinessMember> GetAllBusinessMembersByBusinessJobId(Guid jobId)
        {
            List<Schemas.BusinessMember> businessMembers = new();

            Dictionary<Guid, Schemas.BusinessMember> allMembers = GetAllBusinessMembers();

            foreach (Schemas.LinkBusinessMemberHasJob linkBusinessMemberHasJob in this.LinkBusinessMemberHasJobs)
            {
                if (linkBusinessMemberHasJob.JobId == jobId && allMembers.ContainsKey(linkBusinessMemberHasJob.MemberId))
                {
                    businessMembers.Add(allMembers[linkBusinessMemberHasJob.MemberId]);
                }
            }

            return businessMembers;
        }

        // Get all BusinessTask of a Business
        public Dictionary<Guid, Schemas.BusinessMember> GetAllBusinessMembersByBusinessId(Guid businessId)
        {
            Dictionary<Guid, Schemas.BusinessMember> businessMembers = new Dictionary<Guid, Schemas.BusinessMember>();

            foreach (var linkBusinessToMember in this.LinkBusinessHasMembers)
            {
                if (linkBusinessToMember.BusinessId == businessId && this.BusinessMembers.ContainsKey(linkBusinessToMember.MemberId))
                {
                    businessMembers[linkBusinessToMember.MemberId] = this.BusinessMembers[linkBusinessToMember.MemberId];
                }
            }

            return businessMembers;
        }


        public bool DeleteBusinessMemberById(Guid memberId)
        {
            if (!this.BusinessMembers.ContainsKey(memberId)) return false;

            this.BusinessMembers.Remove(memberId);
            return true;
        }

        #endregion
    }
}
