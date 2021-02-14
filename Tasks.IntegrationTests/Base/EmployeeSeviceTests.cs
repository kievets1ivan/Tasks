using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.BLL.DTOs;
using Tasks.BLL.Services;
using Tasks.DAL.EF;
using Tasks.DAL.Enums;
using Tasks.DAL.Repositories;
using Tasks.DAL.Services;
using Xunit;

namespace Tasks.IntegrationTests.Base
{
    [Collection("DatabaseFixture")]
    public class EmployeeSeviceTests : BaseIntegrationTest
    {

        private readonly DatabaseFixture _fixture;

        public EmployeeSeviceTests(DatabaseFixture fixture) : base()
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_Create_And_Return_Employee_For_AddEmployee()
        {
            var dateTimeProvider = new DateTimeProvider();
            var expected = new EmployeeDTO()
            {
                FirstName = "Test",
                LastName = "qwerty",
                Age = 20
            };
            EmployeeDTO result;

            using (var context = new ApplicationContext(_fixture.DbOptions, dateTimeProvider))
            {
                using var transaction = context.Database.BeginTransaction();

                var employeeService = CreateEmployeeService(context);

                result = await employeeService.AddEmployee(expected);
            }

            Assert.True(result.Id > 0);
            Assert.Equal(expected, result, new GenericComparer<EmployeeDTO>(item =>
            {
                return new
                {
                    item.FirstName,
                    item.LastName,
                    item.Age
                };
            }));
        }

        [Fact]
        public async Task Should_Create_And_Retrieve_Employee()
        {
            var dateTimeProvider = new DateTimeProvider();
            var expected = new EmployeeDTO()
            {
                FirstName = "Test",
                LastName = "qwerty",
                Age = 20
            };
            EmployeeDTO result;

            using (var context = new ApplicationContext(_fixture.DbOptions, dateTimeProvider))
            {
                using var transaction = context.Database.BeginTransaction();

                var employeeService = CreateEmployeeService(context);

                var addedEmployee = await employeeService.AddEmployee(expected);
                result = await employeeService.GetEmployeeById(addedEmployee.Id);
            }

            Assert.True(result.Id > 0);
            Assert.Equal(expected, result, new GenericComparer<EmployeeDTO>(item =>
            {
                return new
                {
                    item.FirstName,
                    item.LastName,
                    item.Age
                };
            }));
        }

        [Fact]
        public async Task Should_Retrieve_Employee_By_Id_For_GetEmployeeById()
        {
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var expected = new EmployeeDTO()
            {
                Id = 2,
                FirstName = "Name2",
                LastName = "Last2",
                Age = 20,
                TaskEmployees = new List<AdditionalTaskEmployeeDTO>
                {
                    new AdditionalTaskEmployeeDTO
                    {
                        AdditionalTaskId = 2,
                        EmployeeId = 2
                    }
                }
            };
            EmployeeDTO result;

            using (var context = new ApplicationContext(_fixture.DbOptions, dateTimeProvider.Object))
            {
                var employeeService = CreateEmployeeService(context);

                result = await employeeService.GetEmployeeById(2);
            }

            Assert.NotNull(result);
            Assert.Equal(expected, result, new GenericComparer<EmployeeDTO>(item =>
            {
                return new
                {
                    item.FirstName,
                    item.LastName,
                    item.Age
                };
            }));
            Assert.Equal(expected.TaskEmployees, result.TaskEmployees, new GenericComparer<AdditionalTaskEmployeeDTO>(item =>
            {
                return new
                {
                    item.AdditionalTaskId,
                    item.EmployeeId
                };
            }));
        }

        [Fact]
        public async Task Should_Update_And_Return_Updated_Employee_For_UpdateEmployee()
        {
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var expected = new EmployeeDTO()
            {
                Id = 3,
                FirstName = "new",
                LastName = "www",
                Age = 23
            };
            EmployeeDTO result;

            using (var context = new ApplicationContext(_fixture.DbOptions, dateTimeProvider.Object))
            {
                var employeeService = CreateEmployeeService(context);

                result = await employeeService.UpdateEmployee(expected);
            }

            Assert.Equal(expected, result, new GenericComparer<EmployeeDTO>(item =>
            {
                return new
                {
                    item.FirstName,
                    item.LastName,
                    item.Age
                };
            }));
        }

        [Fact]
        public async Task Should_Update_And_Retrieve_Updated_Employee_For_UpdateEmployee()
        {
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var expected = new EmployeeDTO()
            {
                Id = 3,
                FirstName = "new",
                LastName = "www",
                Age = 23
            };
            EmployeeDTO result;

            using (var context = new ApplicationContext(_fixture.DbOptions, dateTimeProvider.Object))
            {
                var employeeService = CreateEmployeeService(context);

                var updatedEmployee = await employeeService.UpdateEmployee(expected);
                result = await employeeService.GetEmployeeById(updatedEmployee.Id);
            }

            Assert.Equal(expected, result, new GenericComparer<EmployeeDTO>(item =>
            {
                return new
                {
                    item.FirstName,
                    item.LastName,
                    item.Age
                };
            }));
        }

        [Fact]
        public async Task Should_Delete_Employee_For_DeleteEmployeeById()
        {
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            bool result;

            using (var context = new ApplicationContext(_fixture.DbOptions, dateTimeProvider.Object))
            {
                using var transaction = context.Database.BeginTransaction();

                var employeeService = CreateEmployeeService(context);

                result = await employeeService.DeleteEmployeeById(1);
            }

            Assert.True(result);
        }

        private EmployeeService CreateEmployeeService(ApplicationContext context)
        {
            var dbTransactionService = new DbTransactionService(context);
            var employeeRepository = new EmployeeRepository(context, dbTransactionService);

            return new EmployeeService(employeeRepository, IMapper);
        }
    }
}
