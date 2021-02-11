using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.DAL.EF;
using Tasks.DAL.Entities;
using Tasks.DAL.Services;

namespace Tasks.DAL.Repositories
{
    public interface ITaskRepository
    {
        Task<AdditionalTask> Create(AdditionalTask task);
        Task<bool> Delete(AdditionalTask task);
        Task<IEnumerable<AdditionalTask>> GetAll();
        Task<AdditionalTask> GetById(int taskId, bool asNoTracking = true);
        Task<AdditionalTask> UpdateFull(AdditionalTask task);
        Task<IEnumerable<AdditionalTask>> Update(IEnumerable<AdditionalTask> tasks);
        Task<IEnumerable<AdditionalTask>> GetByIds(IEnumerable<int> ids, bool isFinished, bool asNoTracking = true);
    }

    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationContext _context;
        private readonly IDbTransactionService _transactionService;

        public TaskRepository(ApplicationContext context,
                              IDbTransactionService transactionService)
        {
            _context = context;
            _transactionService = transactionService;
        }

        public async Task<AdditionalTask> GetById(int taskId, bool asNoTracking = true)
        {
            if (asNoTracking)
            {
                return await _context.Tasks.AsNoTracking().Include(e => e.TaskEmployees).ThenInclude(te => te.Employee).SingleOrDefaultAsync(e => e.Id == taskId);
            }

            return await _context.Tasks.Include(e => e.TaskEmployees).ThenInclude(te => te.Employee).SingleOrDefaultAsync(e => e.Id == taskId);
        }

        public async Task<IEnumerable<AdditionalTask>> GetAll() => await _context.Tasks.AsNoTracking().Include(e => e.TaskEmployees).ThenInclude(te => te.Employee).ToListAsync();

        public async Task<AdditionalTask> Create(AdditionalTask task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<AdditionalTask> UpdateFull(AdditionalTask task)
        {
            _transactionService.BeginTransaction();
            try
            {
                _context.AdditionalTaskEmployee.RemoveRange(_context.AdditionalTaskEmployee.Where(x => x.AdditionalTaskId == task.Id));

                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();

                _transactionService.Commit();

                return await GetById(task.Id);
            }
            catch
            {
                _transactionService.RollBack();
                throw;
            }
            finally
            {
                _transactionService.Dispose();
            }
        }

        public async Task<bool> Delete(AdditionalTask task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<AdditionalTask>> Update(IEnumerable<AdditionalTask> tasks)
        {
            _context.Tasks.UpdateRange(tasks);
            await _context.SaveChangesAsync();
            return tasks;
        }

        public async Task<IEnumerable<AdditionalTask>> GetByIds(IEnumerable<int> ids, bool isFinished, bool asNoTracking = true)
        {
            if (asNoTracking)
                return await _context.Tasks.AsNoTracking()
                                           .Include(x => x.TaskEmployees)
                                           .ThenInclude(x => x.Employee)
                                           .Where(x => x.IsFinished == isFinished && ids.Contains(x.Id))
                                           .ToListAsync();
            else
                return await _context.Tasks.Include(x => x.TaskEmployees)
                                           .ThenInclude(x => x.Employee)
                                           .Where(x => x.IsFinished == isFinished && ids.Contains(x.Id))
                                           .ToListAsync();
        }
    }
}
