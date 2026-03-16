using Microsoft.AspNetCore.Mvc;

namespace TrimMiddlewareDemo.Controllers
{
    public class StudentController : Controller
    {
        [HttpPost]
        public IActionResult Post(Student student)
        {
            var name = student.Name;

            // Reverse the string
            char[] arr = name.ToCharArray();
            Array.Reverse(arr);

            var reversed = new string(arr);

            return Ok(reversed);
        }
    }
}
