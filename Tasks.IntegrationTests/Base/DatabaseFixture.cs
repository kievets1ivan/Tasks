using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.DAL.EF;

namespace Tasks.IntegrationTests.Base
{
    public class DatabaseFixture : BaseFixture, IDisposable
    {
        private bool _disposed;

        public DatabaseFixture() : base()
        {
            using var context = new ApplicationContext(DbOptions, new DateTimeProvider());
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            DatabaseInitializer.SeedTestDbData(context);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                using var context = new ApplicationContext(DbOptions, new DateTimeProvider());
                context.Database.EnsureDeleted();
            }
            _disposed = true;
        }
    }
}
