using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Roleplay
{
    public class Table
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TableName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? RemovedAt { get; set; }

        [JsonIgnore]
        public bool IsRemoved { get { return RemovedAt != null; } } 

        public Table()
        {

        }

        public Table(string tableName) 
        {
            this.TableName = tableName;
        }

        public Table(Guid id, string tableName, DateTime createdAt, DateTime removedAt )
        {
            this.Id = id;
            this.TableName = tableName;
            this.CreatedAt = createdAt;
            this.RemovedAt = removedAt;
        }

        public void SoftDelete()
        {
            RemovedAt = DateTime.Now;
        }
    }
}
