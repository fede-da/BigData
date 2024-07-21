using System;
using MongoDB.Driver;
using RagApp.DAL.MongoModels;

namespace RagApp.DAL.Repositories.Mongo.MongoEmployeeService
{
    public interface IMongoEmployeeService
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(string id);
        Task<Employee> CreateAsync(Employee employee);
        Task UpdateAsync(string id, Employee employeeIn);
        Task DeleteAsync(string id);
        Task<long> UpsertEmployeesAsync(IEnumerable<Employee> employees);
    }
}

