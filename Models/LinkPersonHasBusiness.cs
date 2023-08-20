using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Models
{
    public class LinkPersonHasBusiness
    {
        public Guid BusinessId { get; set; }

        public Guid PersonId { get; set; }

        public LinkPersonHasBusiness(Person person, Business business)
        {
            PersonId = person.Id;
            BusinessId = business.Id;
        }
    }
}
