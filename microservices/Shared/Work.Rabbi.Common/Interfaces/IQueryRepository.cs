namespace Work.Rabbi.Common.Interfaces
{
    public interface IQueryRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get All Data using SQL Query or procedure without dapper parameters from single entity
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="isProcedure"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAsync(string sql, bool isProcedure = false);

        /// <summary>
        /// Get All data using SQL Query or Procedure with dapper parameters from a single entity
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="isProcedure"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAsync(string sql, Dictionary<string, object> parameters, bool isProcedure = false);

        /// <summary>
        /// Get All data using SQL Query or Procedure without dapper parameters from two entities
        ///
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="isProcedure"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAsync<TFirst, TSecond, TEntity>(string sql, Func<TFirst, TSecond, TEntity> map, bool isProcedure = false)
            where TFirst : class
            where TSecond : class
            where TEntity : class;

        /// <summary>
        ///  Get All data using SQL Query or Procedure with dapper parameters from two entities
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="parameters"></param>
        /// <param name="isProcedure"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAsync<TFirst, TSecond, TEntity>(string sql, Func<TFirst, TSecond, TEntity> map, Dictionary<string, object> parameters, bool isProcedure = false)
            where TFirst : class
            where TSecond : class
            where TEntity : class;

        /// <summary>
        ///  Get All data using SQL Query or Procedure without dapper parameters from three entities
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="isProcedure"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAsync<TFirst, TSecond, TThird, TEntity>(string sql, Func<TFirst, TSecond, TThird, TEntity> map, string splitters, bool isProcedure = false)
            where TFirst : class
            where TSecond : class
            where TThird : class
            where TEntity : class;

        /// <summary>
        ///  Get All data using SQL Query or Procedure with dapper parameters from three entities
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="parameters"></param>
        /// <param name="isProcedure"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAsync<TFirst, TSecond, TThird, TEntity>(string sql, Func<TFirst, TSecond, TThird, TEntity> map, Dictionary<string, object> parameters, string splitters, bool isProcedure = false)
            where TFirst : class
            where TSecond : class
            where TThird : class
            where TEntity : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="parameters"></param>
        /// <param name="splitters"></param>
        /// <param name="isProcedure"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAsync<TEntity>(string sql, Type[] types, Func<object[], TEntity> map, Dictionary<string, object> parameters, string splitters, bool isProcedure = false)
            where TEntity : class;

        Task<TEntity> SingleAsync(string sql, bool isProcedure = false);

        /// <summary>
        /// Get Single Mapped Data using SQL Query or Procedure without dapper parameters from single entity
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="isProcedure"></param>
        /// <returns></returns>
        Task<TEntity> SingleAsync(string sql, Dictionary<string, object> parameters, bool isProcedure = false);

        /// <summary>
        /// Get Single Mapped Data using SQL Query or Procedure without dapper parameters from two entity
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="isProcedure"></param>
        /// <returns></returns>
        Task<TEntity> SingleAsync<TFirst, TSecond, TEntity>(string sql, Func<TFirst, TSecond, TEntity> map, bool isProcedure = false)
            where TFirst : class
            where TSecond : class;

        /// <summary>
        /// Get Single Mapped Data using SQL Query or Procedure with dapper parameters from two entity
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="parameters"></param>
        /// <param name="isProcedure"></param>
        /// <returns></returns>
        Task<TEntity> SingleAsync<TFirst, TSecond, TEntity>(string sql, Func<TFirst, TSecond, TEntity> map, Dictionary<string, object> parameters, bool isProcedure = false)
            where TFirst : class
            where TSecond : class;

        /// <summary>
        /// Get Single Mapped Data using SQL Query or Procedure without dapper parameters from three entity
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="isProcedure"></param>
        /// <returns></returns>
        Task<TEntity> SingleAsync<TFirst, TSecond, TThird, TEntity>(string sql, Func<TFirst, TSecond, TThird, TEntity> map, bool isProcedure = false)
            where TFirst : class
            where TSecond : class
            where TThird : class
            where TEntity : class;

        /// <summary>
        /// Get Single Mapped Data using SQL Query or Procedure with dapper parameters from three entity
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="parameters"></param>
        /// <param name="isProcedure"></param>
        /// <returns></returns>
        Task<TEntity> SingleAsync<TFirst, TSecond, TThird, TEntity>(string sql, Func<TFirst, TSecond, TThird, TEntity> map, Dictionary<string, object> parameters, bool isProcedure = false) where TFirst : class
            where TSecond : class
            where TThird : class
            where TEntity : class;
    }
}