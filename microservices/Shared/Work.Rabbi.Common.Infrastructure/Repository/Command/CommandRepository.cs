using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Work.Rabbi.Common.Interfaces;
using Work.Rabbi.Common.Infrastructure.Persistence;

namespace Amsl.CleanArchitecture.Infrastructure.Repository.Write.Command;

public class CommandRepository<TEntity, TContext> : IDisposable, ICommandRepository<TEntity, TContext>
    where TEntity : class
    where TContext : DbContext
{    
    private readonly DbFactory<TContext> _dbFactory;
    private DbSet<TEntity> _dbSet;

    public CommandRepository(DbFactory<TContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    protected DbSet<TEntity> DbSet
    {
        get => _dbSet ??= _dbFactory.DbContext.Set<TEntity>();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        DbSet.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(IEnumerable<TEntity> entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        DbSet.RemoveRange(entity);
        await Task.CompletedTask;
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        var result = await DbSet.AddAsync(entity);
        if (result.State != EntityState.Added)
            throw new InvalidOperationException($"{nameof(entity)} didn't added!");

        return entity;
    }

    public async Task<IEnumerable<TEntity>> InsertAsync(IEnumerable<TEntity> entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        await DbSet.AddRangeAsync(entity);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        DbSet.Update(entity);
        await Task.CompletedTask;
        return entity;
    }

    public async Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        DbSet.UpdateRange(entity);
        await Task.CompletedTask;
        return entity;
    }

    public async Task<TEntity> DeleteAsync(object id)
    {
        if (id == null)
        {
            throw new ArgumentNullException(nameof(id));
        }
        var data = await DbSet.FindAsync(id);
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }
        DbSet.Remove(data);
        // TODO: need optimization later
        await Task.CompletedTask;
        return data;
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

    public Task<TEntity> GetAsync(object id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }
    #endregion
}
