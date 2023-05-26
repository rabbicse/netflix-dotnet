using Dapper;
using Microsoft.Extensions.Options;
using System.Data;
using Work.Rabbi.Common.Infrastructure.Configs;
using Work.Rabbi.Common.Infrastructure.Persistence;
using Work.Rabbi.Common.Interfaces;

namespace CleanArchitecture.Base.Infrastructure.Repository.Write.Query;

public class QueryRepository<TEntity> : DbConnector, IDisposable, IQueryRepository<TEntity> where TEntity : class
{
    protected QueryRepository(IOptions<DatabaseSettings> settings, Func<string, IDbConnection> dbConnection)
        : base(settings, dbConnection)
    {
    }

    public async Task<IEnumerable<TEntity>> GetAsync(string sql, bool isProcedure = false)
    {

        using var connection = CreateConnection();
        return await connection.QueryAsync<TEntity>(sql, commandType: isProcedure ? CommandType.StoredProcedure : CommandType.Text);
    }

    public async Task<IEnumerable<TEntity>> GetAsync(string sql, Dictionary<string, object> parameters, bool isProcedure = false)
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync<TEntity>(sql, new DynamicParameters(parameters), commandType: isProcedure ? CommandType.StoredProcedure : CommandType.Text);
    }

    public async Task<IEnumerable<TEntity>> GetAsync<TFirst, TSecond, TEntity>(string sql, Func<TFirst, TSecond, TEntity> map, bool isProcedure = false) where TFirst : class where TSecond : class where TEntity : class
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync(sql, map, commandType: isProcedure ? CommandType.StoredProcedure : CommandType.Text);
    }

    public async Task<IEnumerable<TEntity>> GetAsync<TFirst, TSecond, TEntity>(string sql, Func<TFirst, TSecond, TEntity> map, Dictionary<string, object> parameters, bool isProcedure = false) where TFirst : class where TSecond : class where TEntity : class
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync(sql, map, new DynamicParameters(parameters), commandType: isProcedure ? CommandType.StoredProcedure : CommandType.Text);
    }

    public async Task<IEnumerable<TEntity>> GetAsync<TFirst, TSecond, TThird, TEntity>(string sql, Func<TFirst, TSecond, TThird, TEntity> map, string splitters, bool isProcedure = false)
        where TFirst : class
        where TSecond : class
        where TThird : class
        where TEntity : class
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync(sql, map, commandType: isProcedure ? CommandType.StoredProcedure : CommandType.Text, splitOn: splitters);
    }

    public async Task<IEnumerable<TEntity>> GetAsync<TFirst, TSecond, TThird, TEntity>(string sql, Func<TFirst, TSecond, TThird, TEntity> map, Dictionary<string, object> parameters, string splitters, bool isProcedure = false)
        where TFirst : class
        where TSecond : class
        where TThird : class
        where TEntity : class
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync(sql, map, new DynamicParameters(parameters), commandType: isProcedure ? CommandType.StoredProcedure : CommandType.Text, splitOn: splitters);
    }

    public async Task<IEnumerable<TEntity>> GetAsync<TEntity>(string sql, Type[] types, Func<object[], TEntity> map, Dictionary<string, object> parameters, string splitters, bool isProcedure = false) where TEntity : class
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync(sql, types, map, new DynamicParameters(parameters), commandType: isProcedure ? CommandType.StoredProcedure : CommandType.Text, splitOn: splitters);
    }

    public async Task<TEntity> SingleAsync(string sql, bool isProcedure = false)
    {
        using var connection = CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<TEntity>(sql, commandType: isProcedure ? CommandType.StoredProcedure : CommandType.Text);
    }

    public async Task<TEntity> SingleAsync(string sql, Dictionary<string, object> parameters, bool isProcedure = false)
    {
        using var connection = CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<TEntity>(sql, new DynamicParameters(parameters), commandType: isProcedure ? CommandType.StoredProcedure : CommandType.Text);
    }
    public async Task<TEntity> SingleAsync<TFirst, TSecond, TEntity>(string sql, Func<TFirst, TSecond, TEntity> map, bool isProcedure = false)
       where TFirst : class
       where TSecond : class
    {
        using var connection = CreateConnection();
        return (TEntity)await connection.QueryAsync(sql, map, commandType: isProcedure ? CommandType.StoredProcedure : CommandType.Text);
    }
    public async Task<TEntity?> SingleAsync<TFirst, TSecond, TEntity>(string sql, Func<TFirst, TSecond, TEntity> map, Dictionary<string, object> parameters,
        bool isProcedure = false)
        where TFirst : class
        where TSecond : class
    {
        using var connection = CreateConnection();
        var returnedData = await connection.QueryAsync(sql, map, parameters, commandType: isProcedure ? CommandType.StoredProcedure : CommandType.Text);
        return returnedData == null ? default : returnedData.FirstOrDefault();
    }

    public Task<TEntity> SingleAsync<TFirst, TSecond, TThird, TEntity>(string sql, Func<TFirst, TSecond, TThird, TEntity> map, bool isProcedure = false)
        where TFirst : class
        where TSecond : class
        where TThird : class
        where TEntity : class
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> SingleAsync<TFirst, TSecond, TThird, TEntity>(string sql, Func<TFirst, TSecond, TThird, TEntity> map, Dictionary<string, object> parameters, bool isProcedure = false)
        where TFirst : class
        where TSecond : class
        where TThird : class
        where TEntity : class
    {
        throw new NotImplementedException();
    }

    #region IDisposable
    private bool _disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            // Free unmanaged resources
            // TODO:
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
