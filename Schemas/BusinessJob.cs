using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Schemas
{
    public class BusinessJob : Table
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public BusinessJob() : base("business_job")
        {
            Event.Register(this);
        }

    }
}
