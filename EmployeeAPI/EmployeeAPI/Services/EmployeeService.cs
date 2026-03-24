using EmployeeAPI.Data;
using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EmployeeAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _sqlContext;
        private readonly IMongoCollection<MongoEmployee> _mongoCollection;

        public EmployeeService(
    AppDbContext sqlContext,
    IOptions<MongoDBSettings> mongoSettings)
        {
            _sqlContext = sqlContext;

            // ── Debug: check values are being read ──────────────────
            var settings = mongoSettings.Value;

            Console.WriteLine($"MongoDB ConnectionString : {settings.ConnectionString}");
            Console.WriteLine($"MongoDB DatabaseName     : {settings.DatabaseName}");
            Console.WriteLine($"MongoDB CollectionName   : {settings.CollectionName}");

            // ── Null guards ─────────────────────────────────────────
            if (string.IsNullOrEmpty(settings.ConnectionString))
                throw new ArgumentNullException("MongoDB ConnectionString is null. Check appsettings.json");

            if (string.IsNullOrEmpty(settings.DatabaseName))
                throw new ArgumentNullException("MongoDB DatabaseName is null. Check appsettings.json");

            if (string.IsNullOrEmpty(settings.CollectionName))
                throw new ArgumentNullException("MongoDB CollectionName is null. Check appsettings.json");

            // ── Setup MongoDB ────────────────────────────────────────
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _mongoCollection = database.GetCollection<MongoEmployee>(settings.CollectionName);
        }

        // ── 1. Get all employees from SQL ──────────────────────────────
        public async Task<List<SqlEmployee>> GetFromSqlAsync()
        {
            return await _sqlContext.Employees.ToListAsync();
        }

        // ── 2. Migrate SQL → MongoDB ───────────────────────────────────
        public async Task<string> MigrateToMongoAsync()
        {
            var sqlEmployees = await _sqlContext.Employees.ToListAsync();

            if (!sqlEmployees.Any())
                return "No employees found in SQL Server.";

            int migratedCount = 0;

            foreach (var emp in sqlEmployees)
            {
                // Avoid duplicates — check by SqlId
                var existing = await _mongoCollection
                    .Find(m => m.SqlId == emp.Id)
                    .FirstOrDefaultAsync();

                if (existing == null)
                {
                    var mongoEmp = new MongoEmployee
                    {
                        SqlId = emp.Id,
                        FirstName = emp.FirstName,
                        LastName = emp.LastName,
                        Email = emp.Email,
                        Department = emp.Department,
                        Salary = emp.Salary,
                        JoiningDate = emp.JoiningDate,
                        MigratedAt = DateTime.UtcNow
                    };

                    await _mongoCollection.InsertOneAsync(mongoEmp);
                    migratedCount++;
                }
            }

            return $"Done! {migratedCount} new records migrated to MongoDB.";
        }

        // ── 3. Get all employees from MongoDB ─────────────────────────
        public async Task<List<MongoEmployee>> GetFromMongoAsync()
        {
            return await _mongoCollection.Find(_ => true).ToListAsync();
        }

        // ── 4. Get single employee by MongoDB Id ──────────────────────
        public async Task<MongoEmployee?> GetByIdFromMongoAsync(string id)
        {
            return await _mongoCollection
                .Find(e => e.Id == id)
                .FirstOrDefaultAsync();
        }

        // ── 5. Delete from MongoDB ─────────────────────────────────────
        public async Task DeleteFromMongoAsync(string id)
        {
            await _mongoCollection.DeleteOneAsync(e => e.Id == id);
        }
    }
}