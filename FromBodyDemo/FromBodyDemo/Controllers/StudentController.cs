using Microsoft.AspNetCore.Mvc;
using FromBodyDemo;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    [HttpPost("add")]
    public IActionResult AddStudent([FromBody] Student student)
    {
        string message = $"Student {student.Name} with Marks {student.Marks} added.";

        return Ok(message);
    }
}