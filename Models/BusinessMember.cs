using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Roleplay.Models
{
    public class BusinessMember : Table
    {
        public Guid PersonId { get; set; }

		public BusinessMember() : base("business_member")
		{

		}

		public BusinessMember(Person person) : base("business_member")
        {
            PersonId = person.Id;
        }

		[JsonConstructor]
        public BusinessMember(Guid personId) : base("business_member")
		{
			PersonId = personId;
		}
	}
}
