using Microsoft.AspNetCore.Mvc;

namespace Controller2Controller.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult m1()
        {
            // using ViewBag
            ViewBag.name = "Shivansh";
            ViewBag.college = "LPU";
            return View();
        }

        //public IActionResult Receive()
        //{
        //    if (TempData.ContainsKey("Message"))
        //    {
        //        var msg = TempData["Message"];
        //        TempData.Keep("Message");
        //        ViewData["Message"] = msg?.ToString();
        //    }
        //    else
        //    {
        //        ViewData["Message"] = "No message received";
        //    }
        //    return View();
        //}


    }
}
