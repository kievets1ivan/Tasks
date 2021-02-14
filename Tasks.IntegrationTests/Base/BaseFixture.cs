using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.DAL.EF;

namespace Tasks.IntegrationTests.Base
{
    public class BaseFixture
    {
        static readonly string DbUniqueIndicator = Guid.NewGuid().ToString();
        public DbContextOptions<ApplicationContext> DbOptions { get; private set; }

        protected ServiceProvider ServiceProvider { get; set; }

        public BaseFixture()
        {
            if (ServiceProvider == null)
                ResetServiceProvider();

            ConfigureDbOptions();
        }
        private void ConfigureDbOptions()
        {
            DbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlServer(GetConnectionString()).Options;
        }

        private static string GetConnectionString()
        {
            var config = new ConfigurationBuilder().AddJsonFile("testsettings.json").Build();
            return string.Format(CultureInfo.InvariantCulture, config.GetConnectionString("LocalConnectionString"), DbUniqueIndicator);
        }

        public void ResetServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTestDbContext(GetConnectionString());
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
