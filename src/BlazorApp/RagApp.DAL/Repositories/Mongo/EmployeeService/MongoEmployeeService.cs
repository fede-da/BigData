using System;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using RagApp.DAL.MongoModels;

namespace RagApp.DAL.Repositories.Mongo.MongoEmployeeService
{
    public class MongoEmployeeService : IMongoEmployeeService
    {
        private readonly IMongoCollection<Employee> _employees;

        public MongoEmployeeService(IOptions<CheshireCatDatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _employees = mongoDatabase.GetCollection<Employee>(settings.Value.EmmployeesCollectionName);
        }

        public async Task<List<Employee>> GetAllAsync() =>
            await _employees.Find(_ => true).ToListAsync();

        public async Task<Employee> GetByIdAsync(string id) =>
            await _employees.Find<Employee>(employee => employee.ID == id).FirstOrDefaultAsync();

        public async Task<Employee> CreateAsync(Employee employee)
        {
            await _employees.InsertOneAsync(employee);
            return employee;
        }

        public async Task UpdateAsync(string id, Employee employeeIn) =>
            await _employees.ReplaceOneAsync(employee => employee.ID == id, employeeIn);

        public async Task DeleteAsync(string id) =>
            await _employees.DeleteOneAsync(employee => employee.ID == id);

        // Inserts or updates multiple employees
        public async Task<long> UpsertEmployeesAsync(IEnumerable<Employee> employees)
        {
            var models = new List<WriteModel<Employee>>();
            foreach (var employee in employees)
            {
                var filter = Builders<Employee>.Filter.Eq(e => e.ID, employee.ID);
                if (string.IsNullOrEmpty(employee.ID))
                {
                    // For new documents, MongoDB will automatically generate an _id
                    models.Add(new InsertOneModel<Employee>(employee));
                }
                else
                {
                    // For existing documents, ensure the _id is not altered
                    var updateDefinition = Builders<Employee>.Update
                        .Set(e => e.Name, employee.Name)  // Example: set other fields but do NOT set _id
                        .Set(e => e.Age, employee.Age)
                        .Set(e => e.HiringDate, employee.HiringDate)
                        .Set(e => e.Position, employee.Position)
                        .Set(e => e.Department, employee.Department)
                        .Set(e => e.Email, employee.Email);
                    models.Add(new UpdateOneModel<Employee>(filter, updateDefinition) { IsUpsert = true });
                }
            }

            var operationResult = await _employees.BulkWriteAsync(models);
            return operationResult.InsertedCount + operationResult.ModifiedCount;
        }
    }
}

