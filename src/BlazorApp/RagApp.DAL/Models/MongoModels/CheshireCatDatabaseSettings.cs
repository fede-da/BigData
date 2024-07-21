using System;
namespace RagApp.DAL.MongoModels
{
    public class CheshireCatDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string EmmployeesCollectionName { get; set; } = null!;
    }
}

