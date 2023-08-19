using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Schemas
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

        public LinkBusinessHasMember(Guid memberId, Guid businessId)
        {
            MemberId = memberId;
            BusinessId = businessId;
        }

    }
}
