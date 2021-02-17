using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.DAL.EF
{
    public static class ApplicationDbInitializer
    {
        public static void SeedAdmin(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@soft.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "admin@soft.com",
                    Email = "admin@soft.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "qwerty123").Result;
            }
        }
    }
}
