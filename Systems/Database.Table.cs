using System;
using System.Collections.Generic;
using System.Linq;
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

		public T GetTableById<T>(Guid id) where T : Table
		{
			return (T)this.Tables[id];
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
					if (tableLink.From != null && tableLink.From.Type == typeof(T).Name && tableLink.To != null && tableLink.To.Type == typeof(T).Name)
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

	}
}
