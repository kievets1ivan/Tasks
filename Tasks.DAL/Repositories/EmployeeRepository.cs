using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.DAL.EF;
using Tasks.DAL.Entities;
using Tasks.DAL.Services;

namespace Tasks.DAL.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetById(int employeeId, bool asNoTracking = true);
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> Create(Employee employee);
        Task<Employee> UpdateFull(Employee employee);
        Task<bool> Delete(Employee employee);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationContext _context;
        private readonly IDbTransactionService _transactionService;

        public EmployeeRepository(ApplicationContext context,
                                  IDbTransactionService transactionService)
        {
            _context = context;
            _transactionService = transactionService;
        }

        public async Task<Employee> GetById(int employeeId, bool asNoTracking = true)
        {
            if (asNoTracking)
            {
                return await _context.Employees.AsNoTracking().Include(e => e.TaskEmployees).ThenInclude(te => te.AdditionalTask).SingleOrDefaultAsync(e => e.Id == employeeId);
            }

            return await _context.Employees.Include(e => e.TaskEmployees).ThenInclude(te => te.AdditionalTask).SingleOrDefaultAsync(e => e.Id == employeeId);
        }

        public async Task<IEnumerable<Employee>> GetAll() => await _context.Employees.AsNoTracking().Include(e => e.TaskEmployees).ThenInclude(te => te.AdditionalTask).ToListAsync();

        public async Task<Employee> Create(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateFull(Employee employee)
        {
            _transactionService.BeginTransaction();
            try
            {
                _context.AdditionalTaskEmployee.RemoveRange(_context.AdditionalTaskEmployee.Where(x => x.EmployeeId == employee.Id));

                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();

                _transactionService.Commit();

                return await GetById(employee.Id);
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

        public async Task<bool> Delete(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
