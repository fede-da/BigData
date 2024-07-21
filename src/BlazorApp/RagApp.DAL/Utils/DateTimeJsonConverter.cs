using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RagApp.DAL.Utils
{
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
                {
                    throw new JsonException("Expected StartObject token at the start of a DateTime.");
                }
            }

            DateTime dateTime = default;

            // Read inside the object to find "$date" property
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "$date")
                {
                    reader.Read(); // Read to move to the value
                    dateTime = DateTime.Parse(reader.GetString(), null, System.Globalization.DateTimeStyles.RoundtripKind);
                    break;
                }
            }

            // Ensure we consume the end object token
            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject) ;

            return dateTime;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("$date", value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            writer.WriteEndObject();
        }
    }
}

