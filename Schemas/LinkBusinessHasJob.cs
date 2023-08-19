using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Schemas
{
    public class LinkBusinessHasJob
    {
        public Guid JobId { get; set; }
        public Guid BusinessId { get; set; }
        

        /*public List<Business> GetAllBusiness(Database database)
        {
            List<Business> result = new List<Business>();

            foreach (Business business in database.GetAllBusiness()) 
            { 
                if (business.Id == BusinessId)
                {
                    result.Add(business);
                }
            }

            return result;
        }*/
    }
}
