using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roleplay.Models;

namespace Roleplay
{
#nullable enable
    public partial class Database
    {
        public Dictionary<Guid, Person> Persons { get; set; } = new();
        public List<LinkPersonHasBusiness> LinkPersonHasBusinesses { get; set; } = new List<LinkPersonHasBusiness>();

        public List<LinkPersonHasInvitation> LinkPersonHasInvitations { get; set; } = new List<LinkPersonHasInvitation>();

        #region [CRUD] Person

        public Person CreatePerson(Person person)
        {
            this.Persons[person.Id] = person;
            return this.Persons[person.Id];
        }

        public Person? GetPersonById(Guid id)
        {
            return this.Persons[id];
        }

        public Person? GetPersonBySteamId(long id)
        {
            foreach (var person in this.Persons)
            {
                if (person.Value.SteamId == id)
                {
                    return person.Value;
                }
            }

            return null;
        }




        #endregion
    
        public bool CreateLinkPersonToBusiness(Person person, Business business)
        {
            LinkPersonHasBusiness? linkPersonHasBusiness = GetLinkPersonHasBusiness(person, business);
            if (linkPersonHasBusiness == null)
            {
                this.LinkPersonHasBusinesses.Add(new LinkPersonHasBusiness(person, business));
                return true;
            }

            return false;
        }

        public LinkPersonHasBusiness? GetLinkPersonHasBusiness(Person person, Business business)
        {
            foreach(var linkPersonToBusiness in this.LinkPersonHasBusinesses)
            {
                if (linkPersonToBusiness.PersonId == person.Id && linkPersonToBusiness.BusinessId == business.Id)
                {
                    return linkPersonToBusiness;
                }
            }
            return null;
        }
    }
}
