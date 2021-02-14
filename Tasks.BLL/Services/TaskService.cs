using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.BLL.DTOs;
using Tasks.BLL.Exceptions;
using Tasks.BLL.Models;
using Tasks.DAL.EF;
using Tasks.DAL.Entities;
using Tasks.DAL.Repositories;
using Tasks.DAL.Services;

namespace Tasks.BLL.Services
{
    public interface ITaskService
    {
        Task<AdditionalTaskDTO> AddTask(AdditionalTaskDTO taskDTO);
        Task<bool> DeleteTaskById(int taskId);
        Task<IEnumerable<AdditionalTaskDTO>> GetAll();
        Task<AdditionalTaskDTO> GetTaskById(int taskId);
        Task<AdditionalTaskDTO> UpdateTask(AdditionalTaskDTO taskDTO);
        Task<IEnumerable<AdditionalTaskDTO>> DoTasks(IEnumerable<int> tasksIds, TaskInputParameters request);
    }

    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IDbTransactionService _dbTransactionService;

        public TaskService(ITaskRepository taskRepository,
                           IMapper mapper,
                           IDateTimeProvider dateTimeProvider,
                           IDbTransactionService dbTransactionService)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _dateTimeProvider = dateTimeProvider;
            _dbTransactionService = dbTransactionService;
        }

        public async Task<AdditionalTaskDTO> GetTaskById(int taskId)
        {
            var task = await _taskRepository.GetById(taskId);

            if (task == null)
                throw new TaskNotFoundException();

            return _mapper.Map<AdditionalTaskDTO>(task);
        }

        public async Task<IEnumerable<AdditionalTaskDTO>> GetAll() => _mapper.Map<IEnumerable<AdditionalTaskDTO>>(await _taskRepository.GetAll());

        public async Task<AdditionalTaskDTO> AddTask(AdditionalTaskDTO taskDTO)
        {
            if (taskDTO == null)
                throw new ArgumentNullException(nameof(taskDTO));

            var task = _mapper.Map<AdditionalTask>(taskDTO);
            return _mapper.Map<AdditionalTaskDTO>(await _taskRepository.Create(task));
        }

        public async Task<AdditionalTaskDTO> UpdateTask(AdditionalTaskDTO taskDTO)
        {
            if (taskDTO == null)
                throw new ArgumentNullException(nameof(taskDTO));

            return _mapper.Map<AdditionalTaskDTO>(await _taskRepository.UpdateFull(_mapper.Map<AdditionalTask>(taskDTO)));
        }

        public async Task<bool> DeleteTaskById(int taskId)
        {
            var task = await _taskRepository.GetById(taskId, false);

            if (task == null)
                throw new TaskNotFoundException();

            return await _taskRepository.Delete(task);
        }

        public async Task<IEnumerable<AdditionalTaskDTO>> DoTasks(IEnumerable<int> tasksIds, TaskInputParameters request)
        {
            _dbTransactionService.BeginTransaction();
            try
            {
                if (tasksIds == null)
                    throw new ArgumentNullException(nameof(tasksIds));


                var tasks = await _taskRepository.GetByIds(tasksIds, false, false);

                if (tasks == null)
                    throw new TaskNotFoundException();

                if (tasks.Count() != tasksIds.Count())
                    throw new Exception("Some tasks have already finished or they are not existed");

                tasks.ToList().ForEach(task =>
                {
                    if (request.IsStarted.HasValue)
                    {
                        if (request.IsStarted.Value)
                        {
                            task.Start = _dateTimeProvider.GetCurrentUTC;
                        }
                    }
                    else if (request.IsFinished.HasValue)
                    {
                        if (request.IsFinished.Value)
                        {
                            task.End = _dateTimeProvider.GetCurrentUTC;
                            task.IsFinished = true;
                        }
                    }
                });

                _dbTransactionService.Commit();

                return _mapper.Map<IEnumerable<AdditionalTaskDTO>>(await _taskRepository.Update(tasks));
            }
            catch
            {
                _dbTransactionService.RollBack();
                throw;
            }
            finally
            {
                _dbTransactionService.Dispose();
            }
        }
    }
}
