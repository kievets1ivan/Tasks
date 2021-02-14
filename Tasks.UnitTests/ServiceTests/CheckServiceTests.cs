using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.BLL.Exceptions;
using Tasks.BLL.Services;
using Tasks.DAL.Entities;
using Tasks.DAL.Repositories;
using Tasks.UnitTests.Base;
using Xunit;

namespace Tasks.UnitTests.ServiceTests
{
    public class CheckServiceTests : BaseUnitTest
    {
        [Fact]
        public async Task Should_Throw_Exception_On_Invalid_CheckId_For_GetCheckById()
        {
            var checkRepository = new Mock<ICheckRepository>();
            var taskRepository = new Mock<ITaskRepository>();

            checkRepository.Setup(t => t.GetById(It.IsAny<int>())).ReturnsAsync((Check)null);

            CheckService checkService = new CheckService(checkRepository.Object, taskRepository.Object, IMapper);

            await Assert.ThrowsAsync<CheckNotFoundException>(async () => await checkService.GetCheckById(0));
        }

        [Fact]
        public async Task Should_Return_Valid_Check_For_GetCheckById()
        {
            var checkRepository = new Mock<ICheckRepository>();
            var taskRepository = new Mock<ITaskRepository>();

            checkRepository.Setup(t => t.GetById(It.IsAny<int>())).ReturnsAsync(new Check());

            CheckService checkService = new CheckService(checkRepository.Object, taskRepository.Object, IMapper);

            Assert.NotNull(await checkService.GetCheckById(1));
        }

        [Fact]
        public async Task Should_Return_Valid_Checks_For_GetAll()
        {
            var checkRepository = new Mock<ICheckRepository>();
            var taskRepository = new Mock<ITaskRepository>();

            checkRepository.Setup(t => t.GetAll()).ReturnsAsync(new List<Check>());

            CheckService checkService = new CheckService(checkRepository.Object, taskRepository.Object, IMapper);

            Assert.NotNull(await checkService.GetAll());
        }

        [Fact]
        public async Task Should_Throw_Exception_On_Null_For_AddCheck()
        {
            var checkRepository = new Mock<ICheckRepository>();
            var taskRepository = new Mock<ITaskRepository>();

            CheckService checkService = new CheckService(checkRepository.Object, taskRepository.Object, IMapper);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await checkService.AddCheck(null));
        }

        [Fact]
        public async Task Should_Throw_Exception_On_Invalid_Task_Ids_For_AddCheck()
        {
            var checkRepository = new Mock<ICheckRepository>();
            var taskRepository = new Mock<ITaskRepository>();

            taskRepository.Setup(t => t.GetByIds(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync((IEnumerable<AdditionalTask>)null);

            CheckService checkService = new CheckService(checkRepository.Object, taskRepository.Object, IMapper);

            await Assert.ThrowsAsync<TaskNotFoundException>(async () => await checkService.AddCheck(new List<int> { 0 }));
        }

        [Fact]
        public async Task Should_Throw_Exception_On_Invalid_Some_Task_Ids_For_AddCheck()
        {
            var checkRepository = new Mock<ICheckRepository>();
            var taskRepository = new Mock<ITaskRepository>();

            taskRepository.Setup(t => t.GetByIds(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(new List<AdditionalTask> { new AdditionalTask() });

            CheckService checkService = new CheckService(checkRepository.Object, taskRepository.Object, IMapper);

            await Assert.ThrowsAsync<Exception>(async () => await checkService.AddCheck(new List<int> { 0, 1 }));
        }
    }
}
