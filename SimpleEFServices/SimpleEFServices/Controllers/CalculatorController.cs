using Microsoft.AspNetCore.Mvc;
using SimpleEFServices.Services;

namespace SimpleEFServices.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly CalculatorService _calulator;
        public CalculatorController(CalculatorService calulator)
        {
            _calulator = calulator;
        }
        public IActionResult Add()
        {
            int result = _calulator.Add(5, 3);
            return Content("Result = " + result);
        }
        
    }
}
