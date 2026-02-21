using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCOutputTypes.Models;

namespace MVCOutputTypes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //public IActionResult Student()
        //{
        //    var s = new { Nmae = "Shivansh", Marks = 90 };
        //    return Json(s);
        //}
        public IActionResult Test()
        {
            return NotFound();
        }
        //public IActionResult Square(int? number)
        //{
        //    if (number == null)
        //    {
        //        return Content("Please Provide a number");
        //    }else
        //        return View(number.Value);
        //}

        public IActionResult Student()
        {
            int m1=85;
            int m2=90;
            int m3=88;
            int total=m1+m2+m3;
            var student = new
            {
                Name = "Shivansh",Subject1=m1,Subject2=m2,Subject3=m3,TotalMarks=total
            };
            return Json(student);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
