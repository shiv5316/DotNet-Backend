using Microsoft.AspNetCore.Mvc;
using StudentApp.Data;
using StudentApp.Models;

namespace StudentApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_context.Students.ToList());
        }

        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
                var newStudent = new Student
            {
                Name = student.Name,
                Age = student.Age,
                Dept = student.Dept
            };
            _context.Students.Add(newStudent);
            _context.SaveChanges();
            return Ok(newStudent);
        }
    }
}