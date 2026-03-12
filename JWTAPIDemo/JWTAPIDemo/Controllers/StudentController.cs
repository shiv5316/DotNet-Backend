using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAPIDemo.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        [Authorize]
        public IActionResult GetStudents()
        {
            return Ok(new string[]
            {
            "Arun",
            "Divya",
            "Rahul"
            });
        }
    }
}
