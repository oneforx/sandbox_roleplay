using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Map
{
    public class Region
    {
        public Guid Id = Guid.NewGuid();

        public string Name;

        public Vector3 StartPosition;

        public Vector3 EndPosition;
    }
}
