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
	public class Link<F, T> : Link where F : Table where T : Table
	{
		public Link() : base() { }

		public Link(Guid from, Guid to) : base(from, typeof(F).Name, to, typeof(T).Name) { }
	}

	public class Target
	{
		public Guid Id { get; set; }
		public string Type { get; set; }


		[JsonConstructor]
		public Target(Guid id, string type)
		{
			this.Id = id;
			this.Type = type;
		}
	}

	public class Link : Table
	{
		public Target? From { get; set; }
		public Target? To { get; set; }

		[JsonIgnore]
		public bool IsLink = true;
		
		public Link() { }

		public Link(Guid from, string fromType, Guid to, string toType) : base(fromType +"To"+ toType, "Link")
		{
			From = new Target(from, fromType);
			To = new Target(to, toType);
		}
 
		public Link<F, T> ConvertTo<F,T>() where F : Table where T : Table
		{
			Link<F,T> link = new Link<F, T>(this.From.Id, this.To.Id);
			link.Id = this.Id;
			return link;
		}
	}

}
