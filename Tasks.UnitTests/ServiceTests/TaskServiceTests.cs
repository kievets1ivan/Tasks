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
    public class TaskServiceTests : BaseUnitTest
    {

        [Fact]
        public async Task Should_Throw_Exception_On_Invalid_TaskId_For_GetTaskById()
        {
            var taskRepository = new Mock<ITaskRepository>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var dbTransactionService = new Mock<IDbTransactionService>();

            taskRepository.Setup(t => t.GetById(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync((AdditionalTask)null);

            TaskService taskService = new TaskService(taskRepository.Object, IMapper, dateTimeProvider.Object, dbTransactionService.Object);

            await Assert.ThrowsAsync<TaskNotFoundException>(async () => await taskService.GetTaskById(0));
        }

        [Fact]
        public async Task Should_Return_Valid_Task_For_GetTaskById()
        {
            var taskRepository = new Mock<ITaskRepository>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var dbTransactionService = new Mock<IDbTransactionService>();

            taskRepository.Setup(t => t.GetById(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new AdditionalTask());

            TaskService taskService = new TaskService(taskRepository.Object, IMapper, dateTimeProvider.Object, dbTransactionService.Object);

            Assert.NotNull(await taskService.GetTaskById(1));
        }

        [Fact]
        public async Task Should_Return_Valid_Tasks_For_GetAll()
        {
            var taskRepository = new Mock<ITaskRepository>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var dbTransactionService = new Mock<IDbTransactionService>();

            taskRepository.Setup(t => t.GetAll()).ReturnsAsync(new List<AdditionalTask>());

            TaskService taskService = new TaskService(taskRepository.Object, IMapper, dateTimeProvider.Object, dbTransactionService.Object);

            Assert.NotNull(await taskService.GetAll());
        }

        [Fact]
        public async Task Should_Throw_Exception_On_Null_AdditionalTaskDTO_For_AddTask()
        {
            var taskRepository = new Mock<ITaskRepository>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var dbTransactionService = new Mock<IDbTransactionService>();

            TaskService taskService = new TaskService(taskRepository.Object, IMapper, dateTimeProvider.Object, dbTransactionService.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await taskService.AddTask(null));
        }

        [Fact]
        public async Task Should_Return_Added_Task_For_AddTask()
        {
            var taskRepository = new Mock<ITaskRepository>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var dbTransactionService = new Mock<IDbTransactionService>();

            taskRepository.Setup(t => t.Create(It.IsAny<AdditionalTask>())).ReturnsAsync((AdditionalTask task) => task);

            TaskService taskService = new TaskService(taskRepository.Object, IMapper, dateTimeProvider.Object, dbTransactionService.Object);

            Assert.NotNull(await taskService.AddTask(new AdditionalTaskDTO()));
        }

        [Fact]
        public async Task Should_Throw_Exception_On_Null_AdditionalTaskDTO_For_UpdateTask()
        {
            var taskRepository = new Mock<ITaskRepository>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var dbTransactionService = new Mock<IDbTransactionService>();

            TaskService taskService = new TaskService(taskRepository.Object, IMapper, dateTimeProvider.Object, dbTransactionService.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await taskService.UpdateTask(null));
        }

        [Fact]
        public async Task Should_Return_Added_Task_For_UpdateTask()
        {
            var taskRepository = new Mock<ITaskRepository>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var dbTransactionService = new Mock<IDbTransactionService>();

            taskRepository.Setup(t => t.UpdateFull(It.IsAny<AdditionalTask>())).ReturnsAsync((AdditionalTask task) => task);

            TaskService taskService = new TaskService(taskRepository.Object, IMapper, dateTimeProvider.Object, dbTransactionService.Object);

            Assert.NotNull(await taskService.UpdateTask(new AdditionalTaskDTO()));
        }

        [Fact]
        public async Task Should_Throw_Exception_On_Invalid_TaskId_For_DeleteTaskById()
        {
            var taskRepository = new Mock<ITaskRepository>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var dbTransactionService = new Mock<IDbTransactionService>();

            taskRepository.Setup(t => t.GetById(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync((AdditionalTask)null);

            TaskService taskService = new TaskService(taskRepository.Object, IMapper, dateTimeProvider.Object, dbTransactionService.Object);

            await Assert.ThrowsAsync<TaskNotFoundException>(async () => await taskService.DeleteTaskById(0));
        }

        [Fact]
        public async Task Should_Return_True_For_DeleteTaskById()
        {
            var taskRepository = new Mock<ITaskRepository>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var dbTransactionService = new Mock<IDbTransactionService>();

            taskRepository.Setup(t => t.GetById(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new AdditionalTask());
            taskRepository.Setup(t => t.Delete(It.IsAny<AdditionalTask>())).ReturnsAsync(true);

            TaskService taskService = new TaskService(taskRepository.Object, IMapper, dateTimeProvider.Object, dbTransactionService.Object);

            Assert.True(await taskService.DeleteTaskById(1));
        }
    }
}
