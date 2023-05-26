using Microsoft.EntityFrameworkCore;
using UserManagement.Infrastructure.Persistence;

namespace UserManagement.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbFactory _dbFactory;
        private readonly ICurrentUserService _currentUserService;

        public UnitOfWork(DbFactory dbFactory, ICurrentUserService currentUserService)
        {
            _dbFactory = dbFactory;
            _currentUserService = currentUserService;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await using var transaction = await _dbFactory.DbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                //foreach (var entry in _dbFactory.DbContext.ChangeTracker.Entries<BaseEntity<Guid>>())
                //{
                //    switch (entry.State)
                //    {
                //        case EntityState.Added:
                //            {
                //                // TODO: Custom Action
                //                break;
                //            }
                //        case EntityState.Modified:
                //            {
                //                // TODO: Custom Action
                //                break;
                //            }
                //        case EntityState.Deleted:
                //            break;
                //    }
                //}
                var affectedRows = await _dbFactory.DbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return affectedRows;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
            finally
            {
                await _dbFactory.DbContext.Database.CloseConnectionAsync();
                await _dbFactory.DbContext.DisposeAsync();
            }
        }

        public void Dispose()
        {
            _dbFactory?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
