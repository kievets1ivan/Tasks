using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.DAL.EF;

namespace Tasks.IntegrationTests.Base
{
    public static class TestDbContextExtensions
    {
        public static void AddTestDbContext(this IServiceCollection service, string connection)
        {
            service.AddDbContext<ApplicationContext>(provider => ((DbContextOptionsBuilder<ApplicationContext>)provider)
                .UseSqlServer(connection), ServiceLifetime.Transient);
        }
    }
}
