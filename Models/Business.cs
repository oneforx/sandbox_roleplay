using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roleplay.System;

#nullable enable
namespace Roleplay.Models
{
	public enum LeftReason
	{
		Normal,
		Bann
	}

	public class Business : Table
    {
        public string? Name { get; set; }


		public Business(string name) : base("business")
        {
            Name = name;
        }


        public Job CreateJob(Database database, Job businessJobTemplate)
        {
            Job businessJob = database.CreateJob(businessJobTemplate);
            database.LinkBusinessToJob(Id, businessJob.Id);
            return businessJob;
        }

        public Dictionary<Guid, Job> GetAllJobs(Database database)
        {
            return database.GetAllBusinessJobsByBusinessId(Id);
        }

        public Dictionary<Guid, BusinessMember> GetAllMembers(Database database)
        {
            return database.GetAllBusinessMembersByBusinessId(Id);
        }

        public BusinessMember? GetMemberByPersonId(Database database, Guid personId)
        {
            foreach (var businessMember in database.GetAllBusinessMembersByBusinessId(Id))
            {
                if (businessMember.Value.PersonId == personId)
                {
                    return businessMember.Value;
                }
            }

            return null;
        }
        public BusinessMember? GetMemberByPersonId(Database database, Person person)
        {
            foreach (var businessMember in database.GetAllBusinessMembersByBusinessId(Id))
            {
                if (businessMember.Value.PersonId == person.Id)
                {
                    return businessMember.Value;
                }
            }

            return null;
        }

        public BusinessMember CreateMember(Database database, BusinessMember newBusinessMember)
        {
            BusinessMember businessMember = database.CreateBusinessMember(newBusinessMember, this);
            LinkMember(database, businessMember);
            Person? memberPerson = database.GetPersonById(businessMember.PersonId);
            if (memberPerson != null)
            {
            }
            return businessMember;
        }

        public LinkBusinessHasMember LinkMember(Database database, BusinessMember businessMember)
        {
            return database.LinkBusinessMember(businessMember, this);
        }

        public void SetPersonOwner(Person person)
        {
            Database.Current.LinkPersonToBusiness(person, this);
        }


        public Invitation CreateInvitation(Database database, Invitation invitation)
        {
            Invitation newInvitation = database.CreateInvitation(invitation);
            database.LinkMultiplePersonToInvitation(new Guid[] { invitation.FromId, invitation.ToId }, newInvitation.Id);
            return newInvitation;
        }

    }
}
