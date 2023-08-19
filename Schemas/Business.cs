using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable
namespace Roleplay.Schemas
{
    public class Business : Table
    {
        public string Name { get; set; }

        public Business(string name) : base("business")
        {
            Name = name;
        }

        public Dictionary<Guid, BusinessJob> GetAllJobs(Database database)
        {
            return database.GetAllBusinessJobsByBusinessId(this.Id);
        }

        public Dictionary<Guid, BusinessMember> GetAllMembers(Database database)
        {
            return database.GetAllBusinessMembersByBusinessId(this.Id);
        }

        public BusinessMember? GetMemberByPersonId(Database database, Guid personId)
        {
            foreach (var businessMember in database.GetAllBusinessMembersByBusinessId(this.Id))
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
            foreach (var businessMember in database.GetAllBusinessMembersByBusinessId(this.Id))
            {
                if (businessMember.Value.PersonId == person.Id)
                {
                    return businessMember.Value;
                }
            }

            return null;
        }

        public BusinessMember AddMember(Database database, BusinessMember businessMember)
        {
            return database.CreateBusinessMember(businessMember, this);
        }

        public void SetPersonOwner(Database database, Person person)
        {
            database.CreateLinkPersonToBusiness(person, this);
        }
    }
}
