using EmpUnitTest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmpUnitTest.Repository
{
    public interface IEmpRepo
    {
        Employee GetEmployee(int id);
        List<Employee> GetAllEmployees();
        void AddEmployee(Employee employee);
    }
}
