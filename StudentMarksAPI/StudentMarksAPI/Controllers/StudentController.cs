using Microsoft.AspNetCore.Mvc;

namespace StudentMarksAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly CollegeDbContext _context;

        public StudentController(CollegeDbContext context)
        {
            _context = context;
        }

        private string CalculateGrade(int total)
        {
            if (total >= 90) return "A";
            if (total >= 75) return "B";
            if (total >= 60) return "C";
            if (total >= 40) return "D";
            return "F";
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _context.Students.Select(s => new
            {
                s.Id,
                s.Name,
                s.M1,
                s.M2,
                Total = (s.M1 ?? 0) + (s.M2 ?? 0),
                Grade = CalculateGrade((s.M1 ?? 0) + (s.M2 ?? 0))
            }).ToList();

            return Ok(students);
        }
    }
}