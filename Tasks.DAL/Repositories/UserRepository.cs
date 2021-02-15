using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.DAL.EF;

namespace Tasks.DAL.Repositories
{
    public interface IUserRepository
    {
        Task<bool> CheckPassword(IdentityUser user, string password);
        Task<IdentityUser> FindByEmail(string email);
    }

    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationContext _context;

        public UserRepository(UserManager<IdentityUser> userManager,
                              ApplicationContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IdentityUser> FindByEmail(string email) => await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Email == email);

        public async Task<bool> CheckPassword(IdentityUser user, string password) => await _userManager.CheckPasswordAsync(user, password);

    }
}
