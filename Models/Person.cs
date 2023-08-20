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
        public Guid Id { get; set; } = Guid.NewGuid();
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
    }
}
