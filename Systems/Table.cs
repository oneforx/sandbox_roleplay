using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Roleplay.Systems
{
    public partial class Table
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string TableName { get; set; }

        public string TableType { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? RemovedAt { get; set; }

        [JsonIgnore]
        public bool IsRemoved { get { return RemovedAt != null; } }

        public Table()
        {

        }

        public Table(string tableName)
        {
            TableName = tableName;
			TableType = this.GetType().Name;
        }

        public Table(string tableName, string tableType)
        {
			TableName = tableName;
			TableType = tableType;
		}

		public Table(Guid id, string tableName, string tableType, DateTime createdAt, DateTime removedAt)
        {
            Id = id;
            TableName = tableName;
			TableType = tableType;
            CreatedAt = createdAt;
            RemovedAt = removedAt;
        }

        public void SoftDelete()
        {
            RemovedAt = DateTime.Now;
        }

        public T? TryConvertTo<T>() where T : Table
        {
            if (this.TableType == typeof(T).Name)
            {
                return (T)this;
			}

			return null;
		}

        /*public Dictionary<Guid, Table> GetAllLinkedTable()
        {
            
        }*/

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
