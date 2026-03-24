using EmployeeAPI.Models;

namespace EmployeeAPI.Services
{
    public interface IEmployeeService
    {
        Task<List<SqlEmployee>> GetFromSqlAsync();
        Task<string> MigrateToMongoAsync();
        Task<List<MongoEmployee>> GetFromMongoAsync();
        Task<MongoEmployee?> GetByIdFromMongoAsync(string id);
        Task DeleteFromMongoAsync(string id);
    }
}