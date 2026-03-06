using EmpUnitTest.Models;
using EmpUnitTest.Repository;

namespace EmpUnitTest.Services
{
    public class EmployeeService
    {
        private readonly IEmpRepo _repository;

        public EmployeeService(IEmpRepo repository)
        {
            _repository = repository;
        }

        public Employee GetEmployee(int id)
        {
            return _repository.GetEmployee(id);
        }

        public List<Employee> GetAllEmployees()
        {
            return _repository.GetAllEmployees();
        }

        public void AddEmployee(Employee employee)
        {
            _repository.AddEmployee(employee);
        }
    }
}
