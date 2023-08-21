using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roleplay.Models;
using Sandbox;

#nullable enable
namespace Roleplay.Systems
{
    public partial class Database : BaseNetworkable
    {
        public Dictionary<Guid, Business> Businesses { get; set; } = new Dictionary<Guid, Business>();

        public Dictionary<Guid, Job> Jobs { get; set; } = new Dictionary<Guid, Job>();

        public Dictionary<Guid, BusinessMember> BusinessMembers { get; set; } = new Dictionary<Guid, BusinessMember>();

        public Dictionary<Guid, Task> Tasks { get; set; } = new Dictionary<Guid, Task>();

        #region [CRUD] Business

        public Business CreateBusiness(Business business)
        {
            if (business == null)
            {
                business = new Business(Guid.NewGuid().ToString());
                this.Businesses[business.Id] = business;
                return this.Businesses[business.Id];
            }

            this.Businesses[business.Id] = business;
            return this.Businesses[business.Id];
        }

        public Business CreateBusinessWithOwner(Business business, Guid ownerId)
        {
            Business newBusiness = this.CreateBusiness(business);
            Person? ownerPerson = GetPersonById(ownerId);

			if (ownerPerson != null)
            {
                newBusiness.SetPersonOwner(ownerPerson);
				Event.Run(Events.Business.Server.PersonCreatedBusinessID, ownerPerson, business);
                ResponseOnSelfJoin(business.Serialize());
                return newBusiness;
            }
            else
            {
                throw new Exception("Could not find the Person with ownerId");
            }
        }

        [ClientRpc]
        public static void ResponseOnSelfJoin(string businessData)
		{
			Event.Run(Events.Business.Client.OnSelfJoinID, Business.Deserialize<Business>(businessData));
		}

		public Business CreateBusinessWithOwner(Business business, Person owner)
		{
			Business newBusiness = this.CreateBusiness(business);
			this.LinkPersonToBusiness(owner, newBusiness);
			Event.Run(Events.Business.Server.PersonJoinedBusinessID, owner, business);
			ResponseOnSelfJoin(business.Serialize());
			return newBusiness;
		}

		public Business? GetBusinessById(Guid businessId)
        {
            return this.Businesses[businessId];
        }

        public Dictionary<Guid, Business> GetBusinessByOwnerId(Guid ownerId)
        {
            Dictionary<Guid, Business> businesses = new();
            
            foreach (var personHasBusiness in this.LinkPersonHasBusinesses)
            {
                if (personHasBusiness.PersonId == ownerId)
                {
                    Business? businessFound = this.GetBusinessById(personHasBusiness.BusinessId);
                    if (businessFound == null) throw new Exception("The person is linked to a non-existent business");
                    businesses.Add(personHasBusiness.BusinessId, businessFound);
                }
            }

            return businesses;
        }

        public IDictionary<Guid, Business> GetAllBusiness()
        {
            return this.Businesses;
        }

        public Dictionary<Guid, Business> GetAllBusinessByPersonId(Guid personId)
        {
            Dictionary<Guid, Business> businesses = new();

            foreach (LinkPersonHasBusiness linkPersonHasBusiness in this.LinkPersonHasBusinesses)
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
            Business? business = GetBusinessById(businessId);
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

        #region [CRUD] Job

        public Job CreateJob(Job businessJob)
        {
            this.Jobs.Add(businessJob.Id, businessJob);
            return this.Jobs[businessJob.Id];
        }

        public LinkBusinessHasJob LinkBusinessToJob(Guid businessId, Guid jobId)
        {
            LinkBusinessHasJob linkBusinessHasJob = new(businessId, jobId);
            this.LinkBusinessHasJobs.Add(linkBusinessHasJob);
            return linkBusinessHasJob;
        }

        public Job? GetBusinessJobById(Guid jobId)
        {
            return this.Jobs[jobId];
        }

        public IDictionary<Guid, Job> GetAllBusinessJobs()
        {
            return this.Jobs;
        }

        public Dictionary<Guid, Job> GetAllBusinessJobsByBusinessId(Guid businessId)
        {
            Dictionary<Guid, Job> businessJobs = new();

            foreach (LinkBusinessHasJob linkBusinessHasJob in this.LinkBusinessHasJobs)
            {
                if (linkBusinessHasJob.BusinessId == businessId && this.Jobs.ContainsKey(linkBusinessHasJob.JobId))
                {
                    businessJobs[linkBusinessHasJob.JobId] = this.Jobs[linkBusinessHasJob.JobId];
                }
            }

            return businessJobs;
        }
    
        public bool DeleteBusinessJobById(Guid jobId)
        {
            if (!this.Jobs.ContainsKey(jobId)) return false;

            this.Jobs.Remove(jobId);

            this.LinkBusinessJobHasTasks.ForEach((linkBusinessJobToTask) => DeleteBusinessTaskById(linkBusinessJobToTask.TaskId));

            this.LinkBusinessJobHasTasks.RemoveAll((linkBusinessJobToTask) => linkBusinessJobToTask.JobId == jobId);

            return true;
        }
        
        #endregion

        #region [CRUD] BusinessTask

        public Task CreateTask(Task businessTask)
        {
            this.Tasks[businessTask.Id] = businessTask;
            return this.Tasks[businessTask.Id];
		}
		public LinkJobHasTask LinkBusinessTaskToJob(Guid taskId, Guid jobId)
		{
            LinkJobHasTask linkBusinessToTask = new LinkJobHasTask(jobId, taskId);
            this.LinkBusinessJobHasTasks.Add(linkBusinessToTask);
            return linkBusinessToTask;
		}

		public Task? GetBusinessTaskById(Guid taskId)
        {
            return this.Tasks[taskId];
        }

        public Dictionary<Guid, Task> GetAllBusinessTasks()
        {
            return this.Tasks;
        }

        public Dictionary<Guid, Task> GetAllBusinessTasksByBusinessJobId(Guid jobId)
        {
            Dictionary<Guid, Task> businessTasks = new();

            foreach (LinkJobHasTask linkJobHasTask in this.LinkBusinessJobHasTasks)
            {
                if (linkJobHasTask.JobId == jobId && this.Tasks.ContainsKey(linkJobHasTask.TaskId))
                {
                    businessTasks[linkJobHasTask.TaskId] = this.Tasks[linkJobHasTask.TaskId];
                }
            }

            return businessTasks;
        }

        // Get all BusinessTask of a Business
        public Dictionary<Guid, Task> GetAllBusinessTasksByBusinessId(Guid businessId)
        {
            Dictionary<Guid, Task> businessTasks = new Dictionary<Guid, Task>();

            foreach (var linkBusinessJobToTask in this.LinkBusinessJobHasTasks)
            {
                var linkedJob = this.LinkBusinessHasJobs.Find(linkBusinessToJob =>
                    linkBusinessToJob.BusinessId == businessId && linkBusinessToJob.JobId == linkBusinessJobToTask.JobId);

                if (linkedJob != null && this.Tasks.ContainsKey(linkBusinessJobToTask.TaskId))
                {
                    businessTasks[linkBusinessJobToTask.TaskId] = this.Tasks[linkBusinessJobToTask.TaskId];
                }
            }

            return businessTasks;
        }


        public bool DeleteBusinessTaskById(Guid taskId)
        {
            if (!this.Tasks.ContainsKey(taskId)) return false;

            this.Tasks.Remove(taskId);

            return true;
        }

        #endregion

        #region [CRUD] BusinessMember

        public BusinessMember CreateBusinessMember(BusinessMember businessMember, Business business)
        {
            this.BusinessMembers[businessMember.Id] = businessMember;

            return this.BusinessMembers[businessMember.Id];
        }

        public LinkBusinessHasMember LinkBusinessMember(BusinessMember businessMember, Business business)
        {
            LinkBusinessHasMember newLinkBusinessHasMember = new LinkBusinessHasMember(businessMember, business);
            this.LinkBusinessHasMembers.Add(newLinkBusinessHasMember);
            return newLinkBusinessHasMember;
        }

        public BusinessMember? GetBusinessMemberById(Guid taskId)
        {
            return this.BusinessMembers[taskId];
        }

        public Dictionary<Guid, BusinessMember> GetAllBusinessMembers()
        {
            return this.BusinessMembers;
        }

        public List<BusinessMember> GetAllBusinessMembersByBusinessJobId(Guid jobId)
        {
            List<BusinessMember> businessMembers = new();

            Dictionary<Guid, BusinessMember> allMembers = GetAllBusinessMembers();

            foreach (LinkBusinessMemberHasJob linkBusinessMemberHasJob in this.LinkBusinessMemberHasJobs)
            {
                if (linkBusinessMemberHasJob.JobId == jobId && allMembers.ContainsKey(linkBusinessMemberHasJob.MemberId))
                {
                    businessMembers.Add(allMembers[linkBusinessMemberHasJob.MemberId]);
                }
            }

            return businessMembers;
        }

        // Get all BusinessTask of a Business
        public Dictionary<Guid, BusinessMember> GetAllBusinessMembersByBusinessId(Guid businessId)
        {
            Dictionary<Guid, BusinessMember> businessMembers = new Dictionary<Guid, BusinessMember>();

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
