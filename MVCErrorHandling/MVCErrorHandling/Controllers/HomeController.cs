using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCErrorHandling.Models;

namespace MVCErrorHandling.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TestError()
        {
            int x = 10;
            int y = 0;
            int res = x / y;
            return Content(res.ToString());
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
