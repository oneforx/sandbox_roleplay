using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roleplay.Systems;

namespace Roleplay.Models
{
    public class Task : Table
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Task(string name) : base("business_task")
        {
            Name = name;
        }
    }
}
