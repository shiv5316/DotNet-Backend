using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    JoiningDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "Email", "FirstName", "JoiningDate", "LastName", "Salary" },
                values: new object[,]
                {
                    { 1, "IT", "rahul@company.com", "Rahul", new DateTime(2021, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sharma", 55000m },
                    { 2, "HR", "priya@company.com", "Priya", new DateTime(2020, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Verma", 48000m },
                    { 3, "Finance", "amit@company.com", "Amit", new DateTime(2019, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Singh", 62000m },
                    { 4, "IT", "sneha@company.com", "Sneha", new DateTime(2022, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Patil", 58000m },
                    { 5, "Sales", "vikram@company.com", "Vikram", new DateTime(2021, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Joshi", 45000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
