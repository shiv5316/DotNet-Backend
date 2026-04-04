using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityApi.Interfaces;
using UniversityApi.Models;

namespace UniversityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourse _courseRepository;

        public CourseController(ICourse courseRepository)
        {
            _courseRepository = courseRepository;
        }

        // PUT: api/course
        [HttpPut]
        public IActionResult UpdateCourse([FromBody] Course course)
        {
            if (course == null)
                return BadRequest();

            var result = _courseRepository.UpdateCourse(course);
            if (!result)
                return NotFound();

            return NoContent();
        }

        // GET: api/course/aboveGrade/{grade}
        [HttpGet("aboveGrade/{grade}")]
        public ActionResult<IEnumerable<Course>> GetCoursesAboveGrade(int grade)
        {
            var courses = _courseRepository.GetCoursesWithEnrollmentsAboveGrade(grade);
            return Ok(courses);
        }

        // GET: api/course/byInstructor?name=Name
        [HttpGet("byInstructor")]
        public ActionResult<IEnumerable<Course>> GetCoursesByInstructor([FromQuery] string name)
        {
            var courses = _courseRepository.GetCoursesByInstructorName(name);
            return Ok(courses);
        }
    }
}
