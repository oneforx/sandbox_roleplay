using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roleplay.Systems;

namespace Roleplay.Models
{
    public class Person : Table
    {
        public long SteamId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }

        public Person(long steamId, string firstname, string lastname, int age) : base("person")
        {
            SteamId = steamId;
            Firstname = firstname;
            Lastname = lastname;
            Age = age;
        }

        public void SetOwnerBusiness(Database database, Business business)
        {
            database.LinkPersonToBusiness(this, business);
        }

        public List<Business> GetListOfBusinesses()
        {
            List<Business> businessPersonOwn = this.ListLinkedTo<Business>().ConvertTo<Business>();
            List<Business> allBusinesses = new List<Business>();

            foreach(Business business in businessPersonOwn)
            {
                allBusinesses.Add(business);
            }

			// Search business where the person is member
			foreach (Link<Business, Person> link in Database.Current.GetListOfLinkOfType<Business, Person>())
            {
                if (link.To.Id == this.Id && businessPersonOwn.Find((business) => business.Id == link.From.Id) == null)
                {
					allBusinesses.Add(Database.Current.GetTableById<Business>(link.To.Id));
                }
            }


            return allBusinesses;
        }
    }
}
