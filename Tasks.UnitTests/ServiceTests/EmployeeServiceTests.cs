using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.BLL.DTOs;
using Tasks.BLL.Exceptions;
using Tasks.BLL.Services;
using Tasks.DAL.EF;
using Tasks.DAL.Entities;
using Tasks.DAL.Repositories;
using Tasks.DAL.Services;
using Tasks.UnitTests.Base;
using Xunit;


namespace Tasks.UnitTests.ServiceTests
{
    public class EmployeeServiceTests : BaseUnitTest
    {
        [Fact]
        public async Task Should_Throw_Exception_On_Invalid_EmployeeId_For_GetEmployeeById()
        {
            var employeeRepository = new Mock<IEmployeeRepository>();

            employeeRepository.Setup(t => t.GetById(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync((Employee)null);

            EmployeeService employeeService = new EmployeeService(employeeRepository.Object, IMapper);

            await Assert.ThrowsAsync<EmployeeNotFoundException>(async () => await employeeService.GetEmployeeById(0));
        }

        [Fact]
        public async Task Should_Return_Valid_Employee_For_GetEmployeeById()
        {
            var employeeRepository = new Mock<IEmployeeRepository>();

            employeeRepository.Setup(t => t.GetById(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Employee());

            EmployeeService employeeService = new EmployeeService(employeeRepository.Object, IMapper);

            Assert.NotNull(await employeeService.GetEmployeeById(1));
        }

        [Fact]
        public async Task Should_Return_Valid_Employees_For_GetAll()
        {
            var employeeRepository = new Mock<IEmployeeRepository>();

            employeeRepository.Setup(t => t.GetAll()).ReturnsAsync(new List<Employee>());

            EmployeeService employeeService = new EmployeeService(employeeRepository.Object, IMapper);

            Assert.NotNull(await employeeService.GetAll());
        }

        [Fact]
        public async Task Should_Throw_Exception_On_Null_EmployeeDTO_For_AddEmployee()
        {
            var employeeRepository = new Mock<IEmployeeRepository>();

            EmployeeService employeeService = new EmployeeService(employeeRepository.Object, IMapper);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await employeeService.AddEmployee(null));
        }

        [Fact]
        public async Task Should_Return_Added_Employee_For_AddEmployee()
        {
            var employeeRepository = new Mock<IEmployeeRepository>();

            employeeRepository.Setup(t => t.Create(It.IsAny<Employee>())).ReturnsAsync((Employee employee) => employee);

            EmployeeService employeeService = new EmployeeService(employeeRepository.Object, IMapper);

            Assert.NotNull(await employeeService.AddEmployee(new EmployeeDTO()));
        }

        [Fact]
        public async Task Should_Throw_Exception_On_Null_EmployeeDTO_For_UpdateEmployee()
        {
            var employeeRepository = new Mock<IEmployeeRepository>();

            EmployeeService employeeService = new EmployeeService(employeeRepository.Object, IMapper);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await employeeService.UpdateEmployee(null));
        }

        [Fact]
        public async Task Should_Return_Added_Employee_For_UpdateEmployee()
        {
            var employeeRepository = new Mock<IEmployeeRepository>();

            employeeRepository.Setup(t => t.UpdateFull(It.IsAny<Employee>())).ReturnsAsync((Employee employee) => employee);

            EmployeeService employeeService = new EmployeeService(employeeRepository.Object, IMapper);

            Assert.NotNull(await employeeService.UpdateEmployee(new EmployeeDTO()));
        }

        [Fact]
        public async Task Should_Throw_Exception_On_Invalid_EmployeeId_For_DeleteEmployeeById()
        {
            var employeeRepository = new Mock<IEmployeeRepository>();

            employeeRepository.Setup(t => t.GetById(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync((Employee)null);

            EmployeeService employeeService = new EmployeeService(employeeRepository.Object, IMapper);

            await Assert.ThrowsAsync<EmployeeNotFoundException>(async () => await employeeService.DeleteEmployeeById(0));
        }

        [Fact]
        public async Task Should_Return_True_For_DeleteEmployeeById()
        {
            var employeeRepository = new Mock<IEmployeeRepository>();

            employeeRepository.Setup(t => t.GetById(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Employee());
            employeeRepository.Setup(t => t.Delete(It.IsAny<Employee>())).ReturnsAsync(true);

            EmployeeService employeeService = new EmployeeService(employeeRepository.Object, IMapper);

            Assert.True(await employeeService.DeleteEmployeeById(1));
        }
    }
}
