using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.DAL.EF;
using Tasks.DAL.Entities;

namespace Tasks.DAL.Repositories
{
    public interface ICheckRepository
    {
        Task<Check> Create(Check check);
        Task<Check> GetById(int checkId);
        Task<IEnumerable<Check>> GetAll();
    }

    public class CheckRepository : ICheckRepository
    {
        private readonly ApplicationContext _context;

        public CheckRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Check> Create(Check check)
        {
            await _context.Checks.AddAsync(check);
            await _context.SaveChangesAsync();
            return check;
        }

        public async Task<Check> GetById(int checkId) => await _context.Checks.AsNoTracking().Include(x => x.Payments).SingleOrDefaultAsync(x => x.Id == checkId);

        public async Task<IEnumerable<Check>> GetAll() => await _context.Checks.AsNoTracking().Include(x => x.Payments).ToListAsync();
    }
}
