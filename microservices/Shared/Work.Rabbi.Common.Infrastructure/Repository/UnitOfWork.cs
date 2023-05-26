using Microsoft.EntityFrameworkCore;
using Work.Rabbi.Common.Infrastructure.Persistence;
using Work.Rabbi.Common.Interfaces;
using Worl.Rabbi.Common.Domain;

namespace Work.Rabbi.Common.Infrastructure.Repository
{
    public class UnitOfWork<TContext> : IUnitOfWork, IDisposable where TContext : DbContext
    {
        private readonly DbFactory<TContext> _dbFactory;
        private readonly ILoggedInUserService _loggedInUserService;

        public UnitOfWork(DbFactory<TContext> dbFactory, ILoggedInUserService currentUserService)
        {
            _dbFactory = dbFactory;
            _loggedInUserService = currentUserService;
        }

        public async Task<int> CommitAsync<TKey>()
        {
            await using var transaction = await _dbFactory.DbContext.Database.BeginTransactionAsync();
            try
            {
                foreach (var entry in _dbFactory.DbContext.ChangeTracker.Entries<BaseEntity<TKey>>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            {
                                if (entry is AuditableEntity<TKey>)
                                {
                                    ((AuditableEntity<TKey>)entry.Entity).CreatedBy = _loggedInUserService.UserId;
                                }
                                break;
                            }
                        case EntityState.Modified:
                            {
                                if (entry is AuditableEntity<TKey>)
                                {
                                    ((AuditableEntity<TKey>)entry.Entity).LastModifiedBy = _loggedInUserService.UserId;
                                }
                                break;
                            }
                        case EntityState.Deleted:
                            break;
                    }
                }
                var affectedRows = await _dbFactory.DbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return affectedRows;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await _dbFactory.DbContext.DisposeAsync();
            }
        }

        public async Task<(int, string)> CommitWithErrorMsgAsync()
        {
            await using var transaction = await _dbFactory.DbContext.Database.BeginTransactionAsync();
            try
            {
                var affectedRows = await _dbFactory.DbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return (affectedRows, "Success");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                //throw ex.InnerException;
                return (-1, ex.InnerException?.Message?.ToString() ?? "");
            }
            finally
            {
                await _dbFactory.DbContext.DisposeAsync();
            }

        }

        #region IDisposable
        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // Free unmanaged resources
                _dbFactory?.Dispose();
            }

            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
        #endregion
    }
}