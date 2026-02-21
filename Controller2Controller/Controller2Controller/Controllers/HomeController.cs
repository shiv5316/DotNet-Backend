using System.Diagnostics;
using Controller2Controller.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controller2Controller.Controllers
{
    public class HomeController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        //public IActionResult Send()
        //{
        //    string message = "Hello from HomeController!";
        //    TempData["Message"] = message;
        //    ViewData["Message"] = message;
        //    return View();
        //}
        public IActionResult Index()
        {
            int num = 5;
            return RedirectToAction("Square", "Second", new { n = num });
        }

       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
