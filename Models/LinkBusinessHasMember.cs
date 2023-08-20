using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Roleplay.Models
{
    public class LinkBusinessHasMember
    {
        public Guid MemberId { get; set; }

        public Guid BusinessId { get; set; }


        public LinkBusinessHasMember(BusinessMember businessMember, Business business)
        {
            MemberId = businessMember.Id;
            BusinessId = business.Id;
        }

        [JsonConstructor]
        public LinkBusinessHasMember(Guid memberId, Guid businessId)
        {
            MemberId = memberId;
            BusinessId = businessId;
        }

    }
}
