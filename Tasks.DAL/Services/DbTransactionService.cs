using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.DAL.EF;

namespace Tasks.DAL.Services
{
    public interface IDbTransactionService
    {
        void BeginTransaction();
        void Commit();
        void Dispose();
        void RollBack();
    }

    public class DbTransactionService : IDbTransactionService
    {
        private IDbContextTransaction _transaction;
        private readonly ApplicationContext _dbContext;
        private bool _disposed;

        public DbTransactionService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void RollBack()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing && _transaction != null)
            {
                _transaction.Dispose();
            }

            _disposed = true;
        }

    }
}
