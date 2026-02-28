using System;
using System.Data;
using Microsoft.Data.SqlClient;

class Program
{
    static string conStr = @"Data Source=SHIVANSH-PC\SQLEXPRESS;Initial Catalog=CompanyDB;Integrated Security=True;TrustServerCertificate=True";
    static void Main()
    {
        Console.Write("Enter Department: ");
        string dept = Console.ReadLine();

        GetEmployeesByDepartment(dept);
        GetDepartmentCount(dept);
        GetEmployeeOrders();
        GetDuplicateEmployees();

        Console.ReadLine();
    }

    // 1️⃣ Get Employees
    static void GetEmployeesByDepartment(string dept)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_GetEmpByDept", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Department", dept);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            Console.WriteLine("\nEmployees in Department:");
            while (dr.Read())
            {
                Console.WriteLine($"{dr["EmpId"]} {dr["Name"]} {dr["Phone"]} {dr["Email"]}");
            }
        }
    }

    // 2️⃣ Output Parameter
    static void GetDepartmentCount(string dept)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_GetDeptEmpCount", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Department", dept);

            SqlParameter output = new SqlParameter("@TotalEmployees", SqlDbType.Int);
            output.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(output);

            con.Open();
            cmd.ExecuteNonQuery();

            Console.WriteLine($"\nTotal employees in {dept}: {output.Value}");
        }
    }

    // 3️⃣ Join Report
    static void GetEmployeeOrders()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_GetEmpOrders", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            Console.WriteLine("\nEmployee Order Report:");
            while (dr.Read())
            {
                Console.WriteLine($"{dr["Name"]} | {dr["Department"]} | Order:{dr["OrderId"]} | Amount:{dr["OrderAmount"]} | Date:{dr["OrderDate"]}");
            }
        }
    }

    // 4️⃣ Duplicates
    static void GetDuplicateEmployees()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_GetDuplicateEmp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            Console.WriteLine("\nDuplicate Employees:");
            while (dr.Read())
            {
                Console.WriteLine($"{dr["EmpId"]} {dr["Name"]} {dr["Phone"]} {dr["Email"]}");
            }
        }
    }
}