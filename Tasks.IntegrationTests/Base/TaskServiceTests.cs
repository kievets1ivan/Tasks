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
    public class TaskServiceTests : BaseIntegrationTest
    {

        private readonly DatabaseFixture _fixture;

        public TaskServiceTests(DatabaseFixture fixture) : base()
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_Create_And_Return_Task_For_AddTask()
        {
            var dateTimeProvider = new DateTimeProvider();
            var expected = new AdditionalTaskDTO()
            {
                Title = "Test",
                Description = "qwerty",
                Complexity = TaskComplexity.Easy,
                Payment = 100
            };
            AdditionalTaskDTO result;

            using (var context = new ApplicationContext(_fixture.DbOptions, dateTimeProvider))
            {
                using var transaction = context.Database.BeginTransaction();

                var taskService = CreateTaskService(context, dateTimeProvider);

                result = await taskService.AddTask(expected);
            }

            Assert.True(result.Id > 0);
            Assert.Equal(expected, result, new GenericComparer<AdditionalTaskDTO>(item =>
            {
                return new
                {
                    item.Title,
                    item.Description,
                    item.Complexity,
                    item.Payment
                };
            }));
        }

        [Fact]
        public async Task Should_Create_And_Retrieve_Task()
        {
            var dateTimeProvider = new DateTimeProvider();
            var expected = new AdditionalTaskDTO()
            {
                Title = "Test",
                Description = "qwerty",
                Complexity = TaskComplexity.Easy,
                Payment = 100
            };
            AdditionalTaskDTO result;

            using (var context = new ApplicationContext(_fixture.DbOptions, dateTimeProvider))
            {
                using var transaction = context.Database.BeginTransaction();

                var taskService = CreateTaskService(context, dateTimeProvider);

                var addedTask = await taskService.AddTask(expected);
                result = await taskService.GetTaskById(addedTask.Id);
            }

            Assert.Equal(expected, result, new GenericComparer<AdditionalTaskDTO>(item =>
            {
                return new
                {
                    item.Title,
                    item.Description,
                    item.Complexity,
                    item.Payment
                };
            }));
        }

        [Fact]
        public async Task Should_Retrieve_Task_By_Id_For_GetTaskById()
        {
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var expected = new AdditionalTaskDTO()
            {
                Id = 1,
                Title = "TestTask1",
                Complexity = TaskComplexity.Easy,
                Payment = 100,
                IsFinished = true,
                TaskEmployees = new List<AdditionalTaskEmployeeDTO>
                {
                    new AdditionalTaskEmployeeDTO
                    {
                        AdditionalTaskId = 1,
                        EmployeeId = 1
                    }
                }
            };
            AdditionalTaskDTO result;

            using (var context = new ApplicationContext(_fixture.DbOptions, dateTimeProvider.Object))
            {
                var taskService = CreateTaskService(context, dateTimeProvider.Object);

                result = await taskService.GetTaskById(1);
            }

            Assert.NotNull(result);
            Assert.Equal(expected, result, new GenericComparer<AdditionalTaskDTO>(item =>
            {
                return new
                {
                    item.Title,
                    item.Description,
                    item.Complexity,
                    item.Payment,
                    item.IsFinished
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
        public async Task Should_Update_And_Return_Updated_Task_For_UpdateTask()
        {
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var taskDTO = new AdditionalTaskDTO()
            {
                Id = 1,
                Title = "UpdatedTitle",
                Complexity = TaskComplexity.Medium,
                Payment = 400,
            };
            AdditionalTaskDTO result;

            using (var context = new ApplicationContext(_fixture.DbOptions, dateTimeProvider.Object))
            {
                var taskService = CreateTaskService(context, dateTimeProvider.Object);

                result = await taskService.UpdateTask(taskDTO);
            }

            Assert.Equal(taskDTO, result, new GenericComparer<AdditionalTaskDTO>(item =>
            {
                return new
                {
                    item.Title,
                    item.Description,
                    item.Complexity,
                    item.Payment
                };
            }));
        }

        [Fact]
        public async Task Should_Update_And_Retrieve_Updated_Task_For_UpdateTask()
        {
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var taskDTO = new AdditionalTaskDTO()
            {
                Id = 1,
                Title = "UpdatedTitle",
                Complexity = TaskComplexity.Medium,
                Payment = 400,
            };
            AdditionalTaskDTO result;

            using (var context = new ApplicationContext(_fixture.DbOptions, dateTimeProvider.Object))
            {
                var taskService = CreateTaskService(context, dateTimeProvider.Object);

                var updetesTask = await taskService.UpdateTask(taskDTO);
                result = await taskService.GetTaskById(updetesTask.Id);
            }

            Assert.Equal(taskDTO, result, new GenericComparer<AdditionalTaskDTO>(item =>
            {
                return new
                {
                    item.Title,
                    item.Description,
                    item.Complexity,
                    item.Payment
                };
            }));
        }

        [Fact]
        public async Task Should_Delete_Task_For_DeleteTaskById()
        {
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            bool result;

            using (var context = new ApplicationContext(_fixture.DbOptions, dateTimeProvider.Object))
            {
                using var transaction = context.Database.BeginTransaction();

                var taskService = CreateTaskService(context, dateTimeProvider.Object);

                result = await taskService.DeleteTaskById(1);
            }

            Assert.True(result);
        }

        private TaskService CreateTaskService(ApplicationContext context, IDateTimeProvider dateTimeProvider)
        {
            var dbTransactionService = new DbTransactionService(context);
            var taskRepository = new TaskRepository(context, dbTransactionService);

            return new TaskService(taskRepository, IMapper, dateTimeProvider, dbTransactionService);
        }
    }
}
