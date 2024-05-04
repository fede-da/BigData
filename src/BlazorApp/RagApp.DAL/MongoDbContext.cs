using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RagApp.DAL.MongoModels;


namespace RagApp.DAL
{

    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDbConnection"));
            _database = client.GetDatabase("ragApp");
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
        public IMongoCollection<Rating> Movies => _database.GetCollection<Rating>("Ratings");

    }

}
