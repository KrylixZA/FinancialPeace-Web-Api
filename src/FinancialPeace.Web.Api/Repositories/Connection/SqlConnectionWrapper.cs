using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace FinancialPeace.Web.Api.Repositories.Connection
{
    /// <inheritdoc />
    public sealed class SqlConnectionWrapper : ISqlConnectionWrapper
    {
        private readonly IDbConnection _dbConnection;
        private readonly object _padlock = new object();

        /// <summary>
        /// Initializes a new private instance of itself.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        public SqlConnectionWrapper(IDbConnection dbConnection)
        {
            lock (_padlock)
            {
                _dbConnection = dbConnection;
            }
        }

        /// <inheritdoc />
        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open();
            }
            return _dbConnection.BeginTransaction(isolationLevel);
        }

        /// <inheritdoc />
        public Task<int> ExecuteNonQueryAsync(string query, DynamicParameters? parameters = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open();
            }
            return _dbConnection.ExecuteAsync(query, parameters, transaction, commandTimeout, commandType);
        }

        /// <inheritdoc />
        public Task<T> QueryFirstAsync<T>(string query, DynamicParameters? parameters = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open();
            }
            return _dbConnection.QueryFirstAsync<T>(query, parameters, transaction, commandTimeout, commandType);
        }

        /// <inheritdoc />
        public Task<T> QueryFirstOrDefaultAsync<T>(string query, DynamicParameters? parameters = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open();
            }
            return _dbConnection.QueryFirstOrDefaultAsync<T>(query, parameters, transaction, commandTimeout, commandType);
        }

        /// <inheritdoc />
        public Task<IEnumerable<T>> QueryAsync<T>(string query, DynamicParameters? parameters = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open();
            }
            return _dbConnection.QueryAsync<T>(query, parameters, transaction, commandTimeout, commandType);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _dbConnection.Close();
        }
    }
}