using Microsoft.Extensions.DependencyInjection;
using Tasks.BLL.Filters;
using Tasks.BLL.Services;
using Tasks.DAL.EF;
using Tasks.DAL.Repositories;

namespace Tasks.API.Configs
{
    public static class DIConfig
    {
        public static void RegisterInjections(this IServiceCollection services)
        {
            //services
            services.AddScoped<IEmployeeService, EmployeeService>();

            //repositories
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            //providers
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<CustomExceptionFilterAttribute>();
        }
    }
}
