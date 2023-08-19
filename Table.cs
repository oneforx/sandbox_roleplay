using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay
{
    public class Table
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TableName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? RemovedAt { get; set; }

        public bool IsRemoved { get { return RemovedAt != null; } } 

        public Table(string name) 
        {
            this.TableName = name;
        } 

        public void SoftDelete()
        {
            RemovedAt = DateTime.Now;
        }
    }
}
