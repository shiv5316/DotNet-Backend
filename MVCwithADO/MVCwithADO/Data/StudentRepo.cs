using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using MVCwithADO.Models;
using Microsoft.Extensions.Configuration;

namespace MVCwithADO.Data
{
    public class StudentRepo
    {
        private readonly string _connectionString;

        public StudentRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id, Name, Age, City FROM StudentsMaster";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                Id = (int)reader["Id"],
                                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader["Name"].ToString(),
                                Age = reader.IsDBNull(reader.GetOrdinal("Age")) ? 0 : (int)reader["Age"],
                                City = reader.IsDBNull(reader.GetOrdinal("City")) ? string.Empty : reader["City"].ToString()
                            });
                        }
                    }
                }
            }

            return students;
        }
    }
}
