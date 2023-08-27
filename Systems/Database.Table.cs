using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Roleplay.Models;
using Sandbox;

#nullable enable
namespace Roleplay.Systems
{
	public partial class Database
	{
		public IDictionary<Guid, Table> Tables { get; set; } = new Dictionary<Guid, Table>
		{
			{ Guid.NewGuid(), new Business("My Business") },
		};

		public Table? GetTableById(Guid id)
		{
			if (this.Tables.ContainsKey(id))
				return this.Tables[id];
			else
				return null;
		}

		public void DeleteTable(Guid id)
		{
			if (Database.Current.Tables.ContainsKey(id))
			{
				Database.Current.Tables.Remove(id);
			}
			else
			{
				throw new Exception("Trying to delete the table + " + id.ToString() + " but it doesn't exist on the client.");
			}
		}

		public T? GetTableById<T>(Guid id) where T : Table
		{
			if (this.Tables.ContainsKey(id))
			{
				return (T)this.Tables[id];
			} else
			{
				return null;
			}
		}
		
		public Link<F,T>? GetLinkByIds<F, T>(Guid fromId, Guid toId) where F : Table where T : Table
		{
			Link<F, T> linkWithTypes;
			foreach(Table table in this.Tables.Values)
			{
				if (table.TableType == "Link")
				{
					Link link = (Link)table;
					if (link.From.Id == fromId && link.To.Id == toId)
					{
						linkWithTypes = link.ConvertTo<F, T>();
						return linkWithTypes;
					}
				}
			}
			return null;
		}

		public Dictionary<Guid, T> GetAllTableByType<T>() where T : Table
		{
			Dictionary<Guid, T> result = new Dictionary<Guid, T>();

			foreach(var table in this.Tables.Values)
			{
				if (table.TableType == typeof(T).Name)
				{
					result.Add(table.Id, (T)table);
				}
			}

			return result;
		}

		public class Fruit : Table
		{

		}

		public void CreateGame()
		{
			Person moi = new Person(0, "", "", 11);
			Fruit pomme = new Fruit();
			this.Tables.Add(pomme.Id, pomme);
			
		}

		public Dictionary<Guid, Table> GetAllTableById(Guid id)
		{
			Dictionary<Guid, Table> result = new Dictionary<Guid, Table>();

			foreach(var table in this.Tables)
			{
				if (table.Key == id)
				{
					result.Add(table.Key, table.Value);
				}
			}

			return result;
		}


		public Dictionary<Guid, Link<F, T>> GetDictOfLinkOfType<F, T>() where F : Table where T : Table
		{
			Dictionary<Guid, Link<F, T>> result = new();
			foreach(var table in this.Tables.Values)
			{
				if (table.TableType == "Link")
				{
					Link tableLink = (Link)table;
					if (tableLink.From != null && tableLink.From.Type == typeof(F).Name && tableLink.To != null && tableLink.To.Type == typeof(T).Name)
					{
						result.Add(table.Id, (Link<F, T>)table);
					}
				}
			}
            return result;
		}

		public List<Link<F, T>> GetListOfLinkOfType<F, T>() where F : Table where T : Table
		{
			List<Link<F, T>> result = new();
			foreach (var table in this.Tables.Values)
			{
				if (table.TableType == "Link")
				{
					Link tableLink = (Link)table;
					if (tableLink.From != null && tableLink.From.Type == typeof(T).Name && tableLink.To != null && tableLink.To.Type == typeof(T).Name)
					{
						result.Add((Link<F, T>)table);
					}
				}
			}

			return result;
		}


        public List<Link> GetListOfLinkOfType(Type F, Type T)
        {
            List<Link> result = new();

            foreach (var table in this.Tables.Values)
            {
                if (table.TableType == "Link")
                {
                    Link tableLink = (Link)table;
                    if (tableLink.From != null && tableLink.From.Type ==  F.Name && tableLink.To != null && tableLink.To.Type == T.Name)
                    {
                        result.Add((Link)table);
                    }
                }
            }

            return result;
        }

    }
}
