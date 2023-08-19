using Roleplay.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay
{
    #nullable enable
    public partial class Database
    {
        public Dictionary<Guid, Schemas.Person> Persons { get; set; } = new();

        #region [CRUD] Person

        public Schemas.Person CreatePerson(Person person)
        {
            this.Persons[person.Id] = person;
            return this.Persons[person.Id];
        }

        public Schemas.Person? GetPersonById(Guid id)
        {
            return this.Persons[id];
        }

        public Schemas.Person? GetPersonBySteamId(long id)
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
    
        public bool CreateLinkPersonToBusiness(Schemas.Person person, Schemas.Business business)
        {
            LinkPersonHasBusiness? linkPersonHasBusiness = GetLinkPersonHasBusiness(person, business);
            if (linkPersonHasBusiness == null)
            {
                this.LinkPersonHasBusinesses.Add(new LinkPersonHasBusiness(person, business));
                return true;
            }

            return false;
        }

        public LinkPersonHasBusiness? GetLinkPersonHasBusiness(Schemas.Person person, Schemas.Business business)
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
