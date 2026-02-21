using Microsoft.AspNetCore.Mvc;

namespace Controller2Controller.Controllers
{
    public class SecondController : Controller
    {
        public IActionResult Square(int n)
        {
            int result = n * n;
            ViewData["Number"] = n;
            ViewData["Square"] = result;
            return View();
        }
    }
}
