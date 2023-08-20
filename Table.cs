using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Roleplay
{
    public partial class Table : BaseNetworkable
	{
        [Net]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Net]
        public string TableName { get; set; }
        
        [Net]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Net]
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

		public static T Deserialize<T>(string businessData)
		{
			return Json.Deserialize<T>(businessData);
		}

		public string Serialize()
		{
			return Json.Serialize(this);
		}
	}
}
