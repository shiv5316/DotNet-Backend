using Microsoft.AspNetCore.Mvc;
using OneToMany.Models;
using OneToMany.Data;

namespace OneToMany.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var dept = _context.Departments.ToList();
            return View(dept);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department dept)
        {
            _context.Departments.Add(dept);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
