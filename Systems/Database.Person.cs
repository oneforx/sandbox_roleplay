using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roleplay.Models;

namespace Roleplay.Systems
{
#nullable enable
    public partial class Database
    {
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
    
        public Link<Person, Business> LinkPersonToBusiness(Person person, Business business)
        {
            Link<Person, Business> linkBusiness = new Link<Person, Business>(person.Id, business.Id);
			this.Tables.Add(linkBusiness.Id, linkBusiness);
            return (Link<Person, Business>)this.Tables[linkBusiness.Id];
		}


        public List<Link<Person, Business>> SelectPersonHasBusiness(Person person, Business business)
        {
            List<Link<Person, Business>> result = new List<Link<Person, Business>>();
            
            foreach(var linkPersonToBusiness in this.GetAllLinkOfType<Person, Business>().Values)
            {
                if (linkPersonToBusiness.From.Id == person.Id && linkPersonToBusiness.To.Id == business.Id)
                {
					result.Add(linkPersonToBusiness);
                }
            }

            return result;
        }

    }
}
