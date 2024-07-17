using System.Text.Json.Serialization;
using System.Text.Json;
using MagniseFinAPI.Models;

namespace MagniseFinAPI
{
    public class MappingConverter : JsonConverter<ICollection<Mapping>>
    {
        public override ICollection<Mapping> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var mappings = new List<Mapping>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var name = reader.GetString();
                    reader.Read();

                    var mapping = JsonSerializer.Deserialize<Mapping>(ref reader, options);
                    if (mapping != null)
                    {
                        mapping.Name = name!;
                        mappings.Add(mapping);
                    }
                }
            }

            return mappings;
        }

        public override void Write(Utf8JsonWriter writer, ICollection<Mapping> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            foreach (var mapping in value)
            {
                writer.WritePropertyName(mapping.Name);
                JsonSerializer.Serialize(writer, mapping, options);
            }

            writer.WriteEndObject();
        }
    }
}
