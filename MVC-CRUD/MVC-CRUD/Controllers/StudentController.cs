using Microsoft.AspNetCore.Mvc;
using StudentCRUDMVC.Models;
using Microsoft.Data.SqlClient;

namespace StudentCRUDMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly IConfiguration _config;
        string cs;

        public StudentController(IConfiguration config)
        {
            _config = config;
            cs = _config.GetConnectionString("conn");
        }

        public IActionResult Index()
        {
            List<Student> list = new List<Student>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Student", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new Student
                    {
                        Id = (int)dr["Id"],
                        Name = dr["Name"].ToString(),
                        Age = (int)dr["Age"],
                        Marks = (int)dr["Marks"]
                    });
                }
            }
            return View(list);
        }

        [HttpPost]
        public IActionResult Create(Student s)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Student(Name,Age,Marks) VALUES(@n,@a,@m)", con);

                cmd.Parameters.AddWithValue("@n", s.Name);
                cmd.Parameters.AddWithValue("@a", s.Age);
                cmd.Parameters.AddWithValue("@m", s.Marks);

                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Student WHERE Id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}