using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Schemas
{
    public class BusinessTask : Table
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public BusinessTask() : base("business_task")
        {

        }
    }
}
