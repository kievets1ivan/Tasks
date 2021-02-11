using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tasks.DAL.Entities;

namespace Tasks.DAL.EF
{
    public class ApplicationContext : DbContext
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public ApplicationContext(DbContextOptions options,
                                 IDateTimeProvider dateTimeProvider) : base(options)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public DbSet<AdditionalTask> Tasks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AdditionalTaskEmployee> AdditionalTaskEmployee { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Check> Checks { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).ModifiedOn = _dateTimeProvider.GetCurrentUTC;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedOn = _dateTimeProvider.GetCurrentUTC;
                }
                else
                {
                    entityEntry.Property("CreatedOn").IsModified = false;
                }
            }

            return (await base.SaveChangesAsync(true, cancellationToken));
        }
    }
}
