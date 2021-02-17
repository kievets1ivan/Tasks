using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.BLL.DTOs;
using Tasks.BLL.Exceptions;
using Tasks.DAL.Entities;
using Tasks.DAL.Repositories;

namespace Tasks.BLL.Services
{
    public interface ICheckService
    {
        Task<IEnumerable<CheckDTO>> GetAll();
        Task<CheckDTO> GetCheckById(int checkId);
        Task<CheckDTO> AddCheck(IEnumerable<int> tasksIds);
    }

    public class CheckService : ICheckService
    {
        private readonly ICheckRepository _checkRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public CheckService(ICheckRepository checkRepository,
                            ITaskRepository taskRepository,
                            IMapper mapper)
        {
            _checkRepository = checkRepository;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<CheckDTO> GetCheckById(int checkId)
        {
            var check = await _checkRepository.GetById(checkId);

            if (check == null)
                throw new CheckNotFoundException();

            return _mapper.Map<CheckDTO>(check);
        }

        public async Task<IEnumerable<CheckDTO>> GetAll() => _mapper.Map<IEnumerable<CheckDTO>>(await _checkRepository.GetAll());

        public async Task<CheckDTO> AddCheck(IEnumerable<int> tasksIds)
        {
            if (tasksIds == null)
                throw new ArgumentNullException(nameof(tasksIds));

            var tasks = await _taskRepository.GetByIds(tasksIds, true);

            if (tasks == null)
                throw new TaskNotFoundException();

            if (tasks.Count() != tasksIds.Count())
                throw new Exception("Some tasks have not finished yet or they are not existed");

            var paidTasks = (await GetAll()).Where(x => x.Payments.Any(p => tasks.Any(t => t.Title == p.TaskTitle)));

            if (paidTasks.Any())
                throw new Exception("Some of these tasks have already been paid");

            var check = new Check { Payments = new List<Payment>() };

            foreach (var task in tasks)
            {
                foreach (var taskEmployee in task.TaskEmployees)
                {
                    check.Payments.Add(new Payment
                    {
                        FullName = $"{taskEmployee.Employee.FirstName} {taskEmployee.Employee.LastName}",
                        TaskTitle = taskEmployee.AdditionalTask.Title,
                        Reward = taskEmployee.AdditionalTask.Payment
                    });
                }
                task.IsPaid = true;
            }
            await _taskRepository.Update(tasks);


            return _mapper.Map<CheckDTO>(await _checkRepository.Create(check));
        }
    }
}
