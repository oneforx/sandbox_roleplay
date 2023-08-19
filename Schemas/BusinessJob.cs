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
        public string Name { get; set; }
        public BusinessJob(string name) : base("business_job")
        {
            Event.Register(this);
            Name = name;
        }

    }
}
