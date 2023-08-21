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
            this.Tables[person.Id] = person;
            return (Person)this.Tables[person.Id];
        }

        public Person? GetPersonById(Guid id)
        {
            return (Person)this.Tables[id];
        }

        public Person? GetPersonBySteamId(long id)
        {
            foreach (var person in this.GetAllTableByType<Person>())
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
            
            foreach(var linkPersonToBusiness in this.GetListOfLinkOfType<Person, Business>())
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
