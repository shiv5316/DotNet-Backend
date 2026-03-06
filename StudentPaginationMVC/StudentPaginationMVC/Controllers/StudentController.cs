using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using StudentPaginationMVC.Models;

public class StudentController : Controller
{
    private readonly IConfiguration _config;

    public StudentController(IConfiguration config)
    {
        _config = config;
    }

    public IActionResult Index(int page = 1)
    {
        List<Student> students = new List<Student>();

        string conn = _config.GetConnectionString("DefaultConnection");

        using (SqlConnection con = new SqlConnection(conn))
        {
            SqlCommand cmd = new SqlCommand("GetStudentPaged", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PageNumber", page);
            cmd.Parameters.AddWithValue("@PageSize", 5);

            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                students.Add(new Student
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),
                    Marks = Convert.ToInt32(dr["Marks"]),
                    Department = dr["Department"].ToString()
                });
            }
        }

        ViewBag.Page = page;

        return View(students);
    }
}