using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using RagApp.DAL.Utils;

namespace RagApp.DAL.MongoModels
{
    public class Employee
    {
        [BsonId]  // MongoDB unique identifier
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("_id")]
        public string ID { get; set; }

        [BsonElement("nome")]
        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [BsonElement("posizione")]
        [JsonPropertyName("posizione")]
        public string Position { get; set; }

        [BsonElement("dipartimento")]
        [JsonPropertyName("dipartimento")]
        public string Department { get; set; }

        [BsonElement("eta")]
        [JsonPropertyName("eta")]
        public int Age { get; set; }

        [BsonElement("data_assunzione")]
        [JsonPropertyName("data_assunzione")]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime HiringDate { get; set; }

        [BsonElement("email")]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        public static List<Employee> DeserializeFromJson(string jsonString)
        {
            var ret = BsonSerializer.Deserialize<List<Employee>>(jsonString);
            return ret;
        }
    }
}

