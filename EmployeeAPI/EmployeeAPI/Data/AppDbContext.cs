using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // This tells EF Core to create an "Employees" table
        public DbSet<SqlEmployee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data — EF will insert this on migration
            modelBuilder.Entity<SqlEmployee>().HasData(
                new SqlEmployee
                {
                    Id = 1,
                    FirstName = "Rahul",
                    LastName = "Sharma",
                    Email = "rahul@company.com",
                    Department = "IT",
                    Salary = 55000,
                    JoiningDate = new DateTime(2021, 3, 15)
                },
                new SqlEmployee
                {
                    Id = 2,
                    FirstName = "Priya",
                    LastName = "Verma",
                    Email = "priya@company.com",
                    Department = "HR",
                    Salary = 48000,
                    JoiningDate = new DateTime(2020, 7, 1)
                },
                new SqlEmployee
                {
                    Id = 3,
                    FirstName = "Amit",
                    LastName = "Singh",
                    Email = "amit@company.com",
                    Department = "Finance",
                    Salary = 62000,
                    JoiningDate = new DateTime(2019, 11, 20)
                },
                new SqlEmployee
                {
                    Id = 4,
                    FirstName = "Sneha",
                    LastName = "Patil",
                    Email = "sneha@company.com",
                    Department = "IT",
                    Salary = 58000,
                    JoiningDate = new DateTime(2022, 1, 10)
                },
                new SqlEmployee
                {
                    Id = 5,
                    FirstName = "Vikram",
                    LastName = "Joshi",
                    Email = "vikram@company.com",
                    Department = "Sales",
                    Salary = 45000,
                    JoiningDate = new DateTime(2021, 9, 5)
                }
            );
        }
    }
}
