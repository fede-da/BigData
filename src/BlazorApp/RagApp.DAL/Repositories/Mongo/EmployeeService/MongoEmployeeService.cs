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
            await _employees.Find<Employee>(employee => employee.Id == id).FirstOrDefaultAsync();

        public async Task<Employee> CreateAsync(Employee employee)
        {
            await _employees.InsertOneAsync(employee);
            return employee;
        }

        public async Task UpdateAsync(string id, Employee employeeIn) =>
            await _employees.ReplaceOneAsync(employee => employee.Id == id, employeeIn);

        public async Task DeleteAsync(string id) =>
            await _employees.DeleteOneAsync(employee => employee.Id == id);

        // Inserts or updates multiple employees
        public async Task<long> UpsertEmployeesAsync(IEnumerable<Employee> employees)
        {
            var models = new List<WriteModel<Employee>>();
            foreach (var employee in employees)
            {
                var filter = Builders<Employee>.Filter.Eq(e => e.Id, employee.Id ?? ObjectId.GenerateNewId().ToString());
                var replaceOne = new ReplaceOneModel<Employee>(filter, employee) { IsUpsert = true };
                models.Add(replaceOne);
            }
            var operationResult = await _employees.BulkWriteAsync(models);
            return operationResult.InsertedCount + operationResult.ModifiedCount;
        }
    }
}

