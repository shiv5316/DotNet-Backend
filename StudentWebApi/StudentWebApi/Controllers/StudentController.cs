using Microsoft.AspNetCore.Mvc;
using StudentWebApi.DTO;
using StudentWebApi.Models;

namespace StudentWebApi.Controllers
{
   
        [ApiController]
        [Route("api/[controller]")]
        public class StudentsController : ControllerBase
        {
            private static List<Student> students = new List<Student>();

            // GET: api/students
            [HttpGet]
            public IActionResult GetStudents()
            {
                var result = students.Select(s => new StudentResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    M1 = s.M1,
                    M2 = s.M2,
                    Total = s.Total,
                    Grade = s.Grade
                });

                return Ok(result);
            }

            // POST: api/students
            [HttpPost]
            public IActionResult CreateStudent(CreateStudent request)
            {
                var student = new Student
                {
                    Id = request.Id,
                    Name = request.Name,
                    Age = request.Age
                };

                students.Add(student);

                return Ok(student);
            }

            // PUT: api/students
            [HttpPut]
            public IActionResult UpdateStudent(UpdateStudent request)
            {
                var student = students.FirstOrDefault(s => s.Id == request.Id);

                if (student == null)
                    return NotFound("Student not found");

                student.M1 = request.M1;
                student.M2 = request.M2;

                student.Total = student.M1 + student.M2;
                student.Grade = CalculateGrade(student.Total);

                return Ok(student);
            }

            private string CalculateGrade(int total)
            {
                if (total >= 180) return "A";
                if (total >= 150) return "B";
                if (total >= 120) return "C";
                return "Fail";
            }
        }
    }

