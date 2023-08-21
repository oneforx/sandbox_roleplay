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
        #region [CRUD] Business

        public Business CreateBusiness(Business business)
        {
            if (business == null)
            {
                business = new Business(Guid.NewGuid().ToString());
                this.Tables[business.Id] = business;
                return (Business)this.Tables[business.Id];
            }

            this.Tables[business.Id] = business;
            return (Business)this.Tables[business.Id];
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
            return (Business?)this.Tables[businessId];
        }

        public Dictionary<Guid, Business> GetBusinessByOwnerId(Guid ownerId)
        {
            Dictionary<Guid, Business> personBusinesses = new();
            
            foreach (var link in this.GetDictOfLinkOfType<Person, Business>().Values)
            {
                if (link.From.Id == ownerId)
                {
                    Business? businessFound = this.GetBusinessById(link.To.Id);
                    if (businessFound == null) throw new Exception("The person is linked to a non-existent business");
					personBusinesses.Add(link.To.Id, businessFound);
                }
            }

            return personBusinesses;
        }

        public IDictionary<Guid, Business> GetAllBusiness()
        {
            return GetAllTableByType<Business>();
        }

        public Dictionary<Guid, Business> GetAllBusinessByPersonId(Guid personId)
        {
            Dictionary<Guid, Business> businesses = new();

            foreach (Link<Person, Business> link in this.GetDictOfLinkOfType<Person, Business>().Values)
            {
                if (link.From.Type == "Person" && link.From.Id == personId && this.Tables.ContainsKey(link.To.Id))
                {
                    businesses[link.To.Id] = (Business)this.Tables[link.To.Id];
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
            if (!this.Tables.ContainsKey(businessId)) return false;

            this.Tables.Remove(businessId);

            // Remove all links

            // TODO: 
            // this.Tables.ForEach((linkBusinessToJob) => DeleteBusinessJobById(linkBusinessToJob.JobId));
                
            // this.Tables.RemoveAll((linkJob) => linkJob.BusinessId == businessId);

            return true;
        }

        #endregion

        #region [CRUD] Job

        public Job CreateJob(Job businessJob)
        {
            this.Tables.Add(businessJob.Id, businessJob);
            return (Job)this.Tables[businessJob.Id];
        }

        public Link<Business, Job> LinkBusinessToJob(Guid businessId, Guid jobId)
        {
            Link<Business, Job> linkBusinessToJob = new Link<Business, Job>(businessId, jobId);
            this.Tables.Add(linkBusinessToJob.Id, linkBusinessToJob);
            return linkBusinessToJob;
        }

        public Job? GetJobById(Guid jobId)
        {
            Dictionary<Guid, Job> jobs = this.GetAllTableByType<Job>();
            foreach(var job in jobs.Values)
            {
               if (job.Id == jobId) return job;
            }
            return null;
        }

        public IDictionary<Guid, Job> GetAllJobs()
        {
            return this.GetAllTableByType<Job>();
        }

		public IDictionary<Guid, Job> GetAllBusinessJobs()
		{
			return this.GetAllTableByType<Job>();
		}


		public Dictionary<Guid, Job> GetAllBusinessJobsByBusinessId(Guid businessId)
        {
            Dictionary<Guid, Job> businessJobs = new();

            foreach (Link<Business, Job> linkBusinessHasJob in this.GetListOfLinkOfType<Business, Job>())
            {
                if (linkBusinessHasJob.From.Id == businessId && this.GetAllTableByType<Job>().ContainsKey(linkBusinessHasJob.To.Id))
                {
                    businessJobs[linkBusinessHasJob.To.Id] = (Job)this.Tables[linkBusinessHasJob.To.Id];
                }
            }

            return businessJobs;
        }
    
        public bool DeleteBusinessJobById(Guid jobId)
        {
            if (!this.GetAllTableByType<Job>().ContainsKey(jobId)) return false;

            this.Tables.Remove(jobId);

            foreach (var link in this.GetListOfLinkOfType<Business, Job>())
            {
                if (link.To.Id == jobId)
                {
                    this.Tables.Remove(link.Id);
                }
            }

			foreach (var link in this.GetListOfLinkOfType<Job, Task>())
			{
				if (link.From.Id == jobId)
				{
                    // Remove the link 
					this.Tables.Remove(link.Id);
                    // Remove task tables
                    this.Tables.Remove(link.To.Id);
				}
			}


            return true;
        }
        
        #endregion

        #region [CRUD] BusinessTask

        public Task CreateTask(Task businessTask)
        {
            this.Tables[businessTask.Id] = businessTask;
            return (Task)this.Tables[businessTask.Id];
		}
		public Link<Job, Task> LinkBusinessTaskToJob(Guid taskId, Guid jobId)
		{
			Link<Job, Task> linkBusinessToTask = new Link<Job, Task>(jobId, taskId);
            this.Tables.Add(linkBusinessToTask.Id, linkBusinessToTask);
            return (Link<Job, Task>)this.Tables[linkBusinessToTask.Id];
		}

		public Task? GetBusinessTaskById(Guid taskId)
        {
            return (Task)this.Tables[taskId];
        }

        public Dictionary<Guid, Task> GetAllBusinessTasks()
        {
            return this.GetAllTableByType<Task>();
        }

        public Dictionary<Guid, Task> GetAllBusinessTasksByBusinessJobId(Guid jobId)
        {
            Dictionary<Guid, Task> businessTasks = new();

            foreach (Task linkJobHasTask in this.GetTableById<Job>(jobId))
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
