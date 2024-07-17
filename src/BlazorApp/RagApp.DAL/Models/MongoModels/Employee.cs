using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RagApp.DAL.MongoModels
{
    public class Employee
    {
        [BsonId]  // MongoDB unique identifier
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nome")]
        public string Name { get; set; }

        [BsonElement("posizione")]
        public string Position { get; set; }

        [BsonElement("dipartimento")]
        public string Department { get; set; }

        [BsonElement("eta")]
        public int Age { get; set; }

        [BsonElement("data_assunzione")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime HiringDate { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }
    }
}

