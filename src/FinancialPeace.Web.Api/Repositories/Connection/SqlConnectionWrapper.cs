using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;

namespace FinancialPeace.Web.Api.Repositories.Connection
{
    /// <inheritdoc />
    public sealed class SqlConnectionWrapper : ISqlConnectionWrapper
    {
        private readonly ILogger<SqlConnectionWrapper> _logger;
        private readonly IDbConnection _dbConnection;
        private readonly object _padlock = new object();

        /// <summary>
        /// Initializes a new private instance of itself.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="logger">The logger.</param>
        public SqlConnectionWrapper(
            IDbConnection dbConnection,
            ILogger<SqlConnectionWrapper> logger)
        {
            _logger = logger;
            lock (_padlock)
            {
                _dbConnection = dbConnection;
            }
        }

        /// <inheritdoc />
        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _logger.LogInformation($"BeginTransaction start. IsolationLevel: {isolationLevel.ToString()}");
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open();
            }
            _logger.LogInformation($"BeginTransaction end. IsolationLevel: {isolationLevel.ToString()}");
            return _dbConnection.BeginTransaction(isolationLevel);
        }

        /// <inheritdoc />
        public Task<int> ExecuteNonQueryAsync(string query, DynamicParameters? parameters = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            _logger.LogInformation($"ExecuteNonQueryAsync start");
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _logger.LogInformation("Opening connection");
                _dbConnection.Open();
            }
            _logger.LogInformation($"ExecuteNonQueryAsync end");
            return _dbConnection.ExecuteAsync(query, parameters, transaction, commandTimeout, commandType);
        }

        /// <inheritdoc />
        public Task<T> QueryFirstAsync<T>(string query, DynamicParameters? parameters = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            _logger.LogInformation($"QueryFirstAsync start");
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _logger.LogInformation("Opening connection");
                _dbConnection.Open();
            }
            _logger.LogInformation($"QueryFirstAsync end");
            return _dbConnection.QueryFirstAsync<T>(query, parameters, transaction, commandTimeout, commandType);
        }

        /// <inheritdoc />
        public Task<T> QueryFirstOrDefaultAsync<T>(string query, DynamicParameters? parameters = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            _logger.LogInformation($"QueryFirstOrDefaultAsync start");
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _logger.LogInformation("Opening connection");
                _dbConnection.Open();
            }
            _logger.LogInformation($"QueryFirstOrDefaultAsync start");
            return _dbConnection.QueryFirstOrDefaultAsync<T>(query, parameters, transaction, commandTimeout, commandType);
        }

        /// <inheritdoc />
        public Task<IEnumerable<T>> QueryAsync<T>(string query, DynamicParameters? parameters = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            _logger.LogInformation($"QueryAsync start");
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _logger.LogInformation("Opening connection");
                _dbConnection.Open();
            }
            _logger.LogInformation($"QueryAsync end");
            return _dbConnection.QueryAsync<T>(query, parameters, transaction, commandTimeout, commandType);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _logger.LogInformation($"Dispose start");
            _dbConnection.Close();
            _logger.LogInformation($"Dispose end");
        }
    }
}