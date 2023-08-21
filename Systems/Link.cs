using Roleplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

#nullable enable

namespace Roleplay.Systems
{

	public class Target<T> where T : Table
	{
		public Guid Guid { get; set; }

		public string Type { get; set; } = typeof(T).Name;

		public Target(Guid guid)
		{
			this.Guid = guid;
		}
	}


    public class Link<F, T> : Link, ILink where F : Table where T : Table
	{
		public Link(Guid from, Guid to) : base(from, typeof(F).Name, to, typeof(T).Name) { }
	}

	public class Target
	{
		public Guid Id { get; set; }
		public string Type { get; set; }

		public Target(Guid guid, string type)
		{
			this.Id = guid;
			this.Type = type;
		}
	}

	public interface ILink
	{
		public Target From { get; set; }
		public Target To { get; set; }
	}

	public class Link : Table, ILink
	{
		public Target? From { get; set; }
		public Target? To { get; set; }

		public bool IsLink = true;

		public Link() { }

		public Link(Guid from, string fromType, Guid to, string toType) : base(fromType +"Link"+ toType, "Link")
		{
			From = new Target(from, fromType);
			To = new Target(to, toType);
		}
	}

}
