using Microsoft.AspNetCore.Mvc;

namespace EmpWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> _employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Shivansh", Age = 28, Salary = 55000 },
            new Employee { Id = 2, Name = "Deepak",   Age = 32, Salary = 72000 },
            new Employee { Id = 3, Name = "Rauhan",   Age = 25, Salary = 48000 },
            new Employee { Id = 4, Name = "Mohit",    Age = 30, Salary = 65000 }
        };

        [HttpPost("addbulk")]
        public IActionResult AddBulk([FromBody] List<Employee> employees)
        {
            if (employees == null || employees.Count == 0)
                return BadRequest("Employee list cannot be empty.");

            foreach (var emp in employees)
            {
                emp.Id = _employees.Count + 1; // auto-assign Id
                _employees.Add(emp);
            }

            return Ok(new
            {
                Message = $"{employees.Count} employee(s) added successfully!",
                TotalEmployees = _employees.Count
            });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            if (_employees.Count == 0)
                return NotFound("No employees found.");

            return Ok(_employees);
        }

        [HttpGet("totalsalary")]
        public IActionResult GetTotalSalary()
        {
            decimal totalSalary = _employees.Sum(e => e.Salary);
            decimal avgSalary = _employees.Count > 0
                                  ? totalSalary / _employees.Count
                                  : 0;

            return Ok(new
            {
                TotalEmployees = _employees.Count,
                TotalSalary = totalSalary,
                AverageSalary = Math.Round(avgSalary, 2)
            });
        }
    }
}
