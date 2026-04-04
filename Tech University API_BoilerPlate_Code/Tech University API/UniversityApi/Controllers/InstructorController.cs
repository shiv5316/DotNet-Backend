using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityApi.Interfaces;
using UniversityApi.Models;

namespace UniversityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructor _instructorRepository;

        public InstructorController(IInstructor instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        // POST: api/instructor
        [HttpPost]
        public IActionResult AddInstructor([FromBody] Instructor instructor)
        {
            if (instructor == null)
                return BadRequest();

            var result = _instructorRepository.AddInstructor(instructor);
            if (!result)
                return BadRequest();

            return CreatedAtAction(null, null);
        }

        // GET: api/instructor/withCourseCountAbove/{count}
        [HttpGet("withCourseCountAbove/{count}")]
        public ActionResult<IEnumerable<Instructor>> GetInstructorsWithCourseCountAbove(int count)
        {
            var instructors = _instructorRepository.GetInstructorsWithCourseCountAbove(count);
            return Ok(instructors);
        }

        // GET: api/instructor/mostEnrollments
        [HttpGet("mostEnrollments")]
        public ActionResult<IEnumerable<Instructor>> GetInstructorsWithMostEnrollments()
        {
            var instructors = _instructorRepository.GetInstructorsWithMostEnrollments();
            return Ok(instructors);
        }
    }
}
