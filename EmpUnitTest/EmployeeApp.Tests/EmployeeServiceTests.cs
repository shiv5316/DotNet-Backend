using EmpUnitTest.Services;
using EmpUnitTest.Models;
using EmpUnitTest.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace EmployeeApp.Tests
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        private Mock<IEmpRepo> _repoMock;
        private EmployeeService _service;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IEmpRepo>();
            _service = new EmployeeService(_repoMock.Object);
        }

        [Test]
        public void GetEmployee_ReturnsEmployee()
        {
            var emp = new Employee
            {
                Id = 1,
                Name = "Shivansh",
                Salary = 50000
            };

            _repoMock.Setup(x => x.GetEmployee(1)).Returns(emp);

            var result = _service.GetEmployee(1);

            Assert.That(result.Name, Is.EqualTo("Shivansh"));
        }

        [Test]
        public void AddEmployee_CallsRepositoryAddMethod()
        {
            var emp = new Employee
            {
                Id = 3,
                Name = "Jane",
                Salary = 55000
            };

            _service.AddEmployee(emp);

            _repoMock.Verify(x => x.AddEmployee(emp), Times.Once);
        }
    }
}
