using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentService _service;

    public StudentController(IStudentService service)
    {
        _service = service;
    }

    // Read All Students
    [HttpGet("AllStudents")]
    public IActionResult GetAllStudents()
    {
        var data = _service.GetAllStudents();
        return Ok(data);
    }

    // Students in hostel
    [HttpGet("HostelStudents")]
    public IActionResult GetStudentsInHostel()
    {
        var data = _service.GetStudentsInHostel();
        return Ok(data);
    }

    // Update Room
    [HttpPut("UpdateRoom")]
    public IActionResult UpdateRoom(int studentId, int roomNo)
    {
        _service.UpdateRoom(studentId, roomNo);
        return Ok("Room Updated");
    }

    // Delete Student
    [HttpDelete("DeleteStudent")]
    public IActionResult DeleteStudent(int studentId)
    {
        _service.DeleteStudent(studentId);
        return Ok("Student Deleted");
    }
}