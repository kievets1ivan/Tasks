using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Tasks.DAL.EF;
using Tasks.DAL.Entities;

namespace Tasks.DAL.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetById(int employeeId, bool asNoTracking = true);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationContext _context;

        public EmployeeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetById(int employeeId, bool asNoTracking = true)
        {
            if (asNoTracking)
            {
                return await _context.Employees.AsNoTracking().SingleOrDefaultAsync(e => e.Id == employeeId);
            }

            return await _context.Employees.SingleOrDefaultAsync(e => e.Id == employeeId);
        }
    }
}
