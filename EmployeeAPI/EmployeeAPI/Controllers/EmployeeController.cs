using EmployeeAPI.Models;
using EmployeeAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        // GET: api/employee/sql
        [HttpGet("sql")]
        public async Task<IActionResult> GetFromSql()
        {
            var result = await _service.GetFromSqlAsync();
            return Ok(result);
        }

        // POST: api/employee/migrate
        [HttpPost("migrate")]
        public async Task<IActionResult> Migrate()
        {
            var result = await _service.MigrateToMongoAsync();
            return Ok(new { message = result });
        }

        // GET: api/employee/mongo
        [HttpGet("mongo")]
        public async Task<IActionResult> GetFromMongo()
        {
            var result = await _service.GetFromMongoAsync();
            return Ok(result);
        }

        // GET: api/employee/mongo/{id}
        [HttpGet("mongo/{id}")]
        public async Task<IActionResult> GetByIdFromMongo(string id)
        {
            var result = await _service.GetByIdFromMongoAsync(id);
            if (result == null)
                return NotFound(new { message = "Employee not found." });

            return Ok(result);
        }

        // DELETE: api/employee/mongo/{id}
        [HttpDelete("mongo/{id}")]
        public async Task<IActionResult> DeleteFromMongo(string id)
        {
            await _service.DeleteFromMongoAsync(id);
            return Ok(new { message = "Deleted successfully." });
        }
    }
}
