using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Schemas
{
    public class Invitation : Table
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Invitation() : base("invitation")
        {

        }

    }
}
