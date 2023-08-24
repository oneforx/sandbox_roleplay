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

        public List<Table> ListLinkedFrom<F>() where F : Table
        {
            List<Table> list = new List<Table>();
            foreach (Link<F, Table> link in Database.Current.GetListOfLinkOfType(typeof(F), Database.Types[this.TableType]))
            {
                if (link.From.Type == typeof(F).Name)
                {
                    list.Add(Database.Current.GetTableById<F>(link.To.Id));
                }
            }
            return list;
        }

        public bool IsOwnerOf(Guid tableId)
        {
            bool found = false;
            foreach(Link link in Database.Current.GetAllTableByType<Link>().Values)
            {
                if (link.From.Id == this.Id && link.To.Id == tableId)
                {
                    found = true;
                    break;
                }
            }

            return found;
        }

		public bool IsMemberOf(Guid tableId)
		{
			bool found = false;
			foreach (Link link in Database.Current.GetAllTableByType<Link>().Values)
			{
				if (link.From.Id == tableId && link.To.Id == this.Id)
				{
					found = true;
					break;
				}
			}

			return found;
		}

		public List<Table> ListLinkedTo<T>() where T : Table
        {
            List<Table> list = new List<Table>();
            foreach (Link link in Database.Current.GetListOfLinkOfType(Database.Types[this.TableType], typeof(T)))
            {
                if (link.To.Type == typeof(T).Name)
                {
                    list.Add(Database.Current.GetTableById<T>(link.To.Id));
                }
            }
            return list;
        }

        /*public Dictionary<Guid, Table> GetAllLinkedTable()
        {
            
        }*/

        public void UpdateClient()
		{
			Database.UpdateTableOnClient(To.Everyone, this.Serialize());
		}

        public static T Deserialize<T>(string businessData)
        {
            return Json.Deserialize<T>(businessData);
        }


        public string Serialize()
        {
            return Json.Serialize((object)this);
        }
    }

}
