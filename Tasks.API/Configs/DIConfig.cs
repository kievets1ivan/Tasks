using Microsoft.Extensions.DependencyInjection;
using Tasks.BLL.Filters;
using Tasks.BLL.Services;
using Tasks.DAL.EF;
using Tasks.DAL.Repositories;
using Tasks.DAL.Services;

namespace Tasks.API.Configs
{
    public static class DIConfig
    {
        public static void RegisterInjections(this IServiceCollection services)
        {
            //services
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IDbTransactionService, DbTransactionService>();
            services.AddScoped<ICheckService, CheckService>();
            services.AddScoped<IUserService, UserService>();

            //repositories
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ICheckRepository, CheckRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            //providers
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<CustomExceptionFilterAttribute>();
        }
    }
}
