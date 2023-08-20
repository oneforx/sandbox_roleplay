using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Models
{
    public class BusinessMember : Table
    {
        public Guid PersonId { get; set; }

        public BusinessMember(Person person) : base("business_member")
        {
            PersonId = person.Id;
        }
    }
}
