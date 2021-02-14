using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.DAL.EF;
using Tasks.DAL.Entities;
using Tasks.DAL.Enums;

namespace Tasks.IntegrationTests.Base
{
    public static class DatabaseInitializer
    {

        public static void SeedTestDbData(ApplicationContext context)
        {
            var tasks = new List<AdditionalTask>
            {
                new AdditionalTask { Id = 1, Title = "TestTask1", Complexity = TaskComplexity.Easy, Payment = 100, IsFinished = true },
                new AdditionalTask { Id = 2, Title = "TestTask2", Complexity = TaskComplexity.Easy, Payment = 100 },
                new AdditionalTask { Id = 3, Title = "TestTask3", Complexity = TaskComplexity.Medium, Payment = 200, IsFinished = true },
                new AdditionalTask { Id = 4, Title = "TestTask4", Complexity = TaskComplexity.Medium, Payment = 200 },
                new AdditionalTask { Id = 5, Title = "TestTask5", Complexity = TaskComplexity.Strong, Payment = 300 },
                new AdditionalTask { Id = 6, Title = "TestTask6", Complexity = TaskComplexity.Strong, Payment = 300 }
            };
            context.Tasks.AddRange(tasks);

            context.Database.OpenConnection();
            try
            {
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Tasks] ON");
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Tasks] OFF");
            }
            finally
            {
                context.Database.CloseConnection();
            }

            var employees = new List<Employee>
            {
                new Employee { Id = 1, FirstName = "Name1",  LastName = "Last1", Age = 19 },
                new Employee { Id = 2, FirstName = "Name2",  LastName = "Last2", Age = 20 },
                new Employee { Id = 3, FirstName = "Name3",  LastName = "Last3", Age = 23 },
                new Employee { Id = 4, FirstName = "Name4",  LastName = "Last4", Age = 24 },
                new Employee { Id = 5, FirstName = "Name5",  LastName = "Last5", Age = 26 }
            };
            context.Employees.AddRange(employees);

            context.Database.OpenConnection();
            try
            {
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Employees] ON");
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Employees] OFF");
            }
            finally
            {
                context.Database.CloseConnection();
            }

            var additionalTaskEmployees = new List<AdditionalTaskEmployee>
            {
                new AdditionalTaskEmployee { Id = 1, AdditionalTaskId = 1, EmployeeId = 1 },
                new AdditionalTaskEmployee { Id = 2, AdditionalTaskId = 2, EmployeeId = 1 },
                new AdditionalTaskEmployee { Id = 3, AdditionalTaskId = 2, EmployeeId = 2 },
                new AdditionalTaskEmployee { Id = 4, AdditionalTaskId = 2, EmployeeId = 3 },
                new AdditionalTaskEmployee { Id = 5, AdditionalTaskId = 3, EmployeeId = 5 },
                new AdditionalTaskEmployee { Id = 6, AdditionalTaskId = 4, EmployeeId = 4 }
            };
            context.AdditionalTaskEmployee.AddRange(additionalTaskEmployees);

            context.Database.OpenConnection();
            try
            {
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[AdditionalTaskEmployee] ON");
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[AdditionalTaskEmployee] OFF");
            }
            finally
            {
                context.Database.CloseConnection();
            }
        }
    }
}
