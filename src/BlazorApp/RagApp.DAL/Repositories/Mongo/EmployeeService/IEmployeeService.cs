using System;
using RagApp.DAL.MongoModels;

namespace RagApp.DAL.Repositories.Mongo.EmployeeService
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(string id);
        Task<Employee> CreateAsync(Employee employee);
        Task UpdateAsync(string id, Employee employeeIn);
        Task DeleteAsync(string id);
    }
}

