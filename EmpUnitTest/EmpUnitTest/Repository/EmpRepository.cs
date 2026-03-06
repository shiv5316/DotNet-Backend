using EmpUnitTest.Models;

namespace EmpUnitTest.Repository
{
    public class EmpRepository : IEmpRepo
    {
        // Simulate data storage
        private static List<Employee> _employees = new()
        {
            new Employee { Id = 1, Name = "Shivansh", Salary = 50000 },
            new Employee { Id = 2, Name = "John", Salary = 60000 }
        };

        public Employee GetEmployee(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        public List<Employee> GetAllEmployees()
        {
            return _employees;
        }

        public void AddEmployee(Employee employee)
        {
            if (employee != null)
            {
                _employees.Add(employee);
            }
        }
    }
}