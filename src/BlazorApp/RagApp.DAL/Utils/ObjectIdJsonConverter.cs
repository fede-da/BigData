using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RagApp.DAL.Utils
{
    public class ObjectIdJsonConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            

            string oid = null;

            // Read inside the object to find "$oid" property
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "$oid")
                {
                    reader.Read(); // Read to move to the value of "$oid"
                    oid = reader.GetString();
                    break;
                }
            }

            // Ensure we consume the end object token
            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject) ;

            if (oid == null)
            {
                throw new JsonException("ObjectId not found or invalid.");
            }

            return oid;
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("$oid", value);
            writer.WriteEndObject();
        }
    }


}

