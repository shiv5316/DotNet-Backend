using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityApi.Interfaces;
using UniversityApi.Models;

namespace UniversityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent _studentRepository;

        public StudentController(IStudent studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // GET: api/student/byCourse?title=CourseTitle
        [HttpGet("byCourse")]
        public ActionResult<IEnumerable<Student>> GetStudentsByCourse([FromQuery] string title)
        {
            var students = _studentRepository.GetStudentsByCourseTitle(title);
            return Ok(students);
        }

        // DELETE: api/student/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var result = _studentRepository.DeleteStudent(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
