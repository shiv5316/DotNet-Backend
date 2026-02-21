using Microsoft.AspNetCore.Mvc;
using MVCwithADO.Data;

namespace MVCwithADO.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentRepo _repo;
        public StudentController(StudentRepo repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            var students = _repo.GetAllStudents();
            return View(students);
        }
    }
}
