using Microsoft.Extensions.Options;
using System.Data;
using Work.Rabbi.Common.Infrastructure.Configs;

namespace Work.Rabbi.Common.Infrastructure.Persistence
{

    public class DbConnector
    {
        private readonly DatabaseSettings _settings;
        private readonly Func<string, IDbConnection> _dbConnection;

        protected DbConnector(IOptions<DatabaseSettings> settings, Func<string, IDbConnection> dbConnection)
        {
            _settings = settings.Value;
            _dbConnection = dbConnection;
        }

        public IDbConnection CreateConnection()
        {
            return _dbConnection(_settings.ConnectionStrings?.DbConnection);
        }
    }
}