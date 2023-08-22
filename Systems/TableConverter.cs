using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Roleplay.Systems
{
	public class TableConverter : JsonConverter<Table>
	{

		public override Table Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
			{
				var root = doc.RootElement;

				// Supposons que le "TableType" est utilisé pour différencier les types dérivés.
				if (root.TryGetProperty("TableType", out var typeProp))
				{
					if (Database.Types.TryGetValue(typeProp.GetString(), out var targetType) )
					{
						if (typeProp.GetString() == "Link")
						{
							string fromType = root.GetProperty("From").GetProperty("Type").GetString();
							string toType = root.GetProperty("To").GetProperty("Type").GetString();

							string linkTypeName = fromType + "To" + toType;

							if (Database.LinkTypes.ContainsKey(linkTypeName))
							{
								var linkType = Database.LinkTypes[linkTypeName];
								return (Link)JsonSerializer.Deserialize(root.GetRawText(), linkType);	
							}
							else
							{
								throw new Exception("Error: " + linkTypeName + " not registered. Could not parse");
							}
						}
						return (Table)JsonSerializer.Deserialize(root.GetRawText(), targetType);
					}
				}

				throw new JsonException();
			}
		}
		public override void Write(Utf8JsonWriter writer, Table value, JsonSerializerOptions options)
		{
			JsonSerializer.Serialize(writer, (object)value, options);
		}
	}
}
