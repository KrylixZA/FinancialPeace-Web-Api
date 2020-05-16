using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace FinancialPeace.Web.Api.Repositories.Connection
{
    /// <summary>
    /// Provides a wrapper for the SQL connection to allow for unit testing when using unit testing.
    /// </summary>
    public interface ISqlConnectionWrapper : IDisposable
    {
        /// <summary>
        /// Creates a transaction based on the open connection.
        /// </summary>
        /// <param name="isolationLevel">The isolation level. By default this is set to ReadCommitted.</param>
        /// <returns></returns>
        IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        /// <summary>
        /// Executes a non-query asynchronously.
        /// </summary>
        /// <param name="query">The query to be run.</param>
        /// <param name="parameters">The parameters to pass into the query.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="commandTimeout">The command timeout in seconds.</param>
        /// <param name="commandType">The command type.</param>
        Task<int> ExecuteNonQueryAsync(string query, DynamicParameters? parameters = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Queries the database and returns the first result from the result set. Throws a null reference exception of nothing is returned from the database.
        /// </summary>
        /// <typeparam name="T">The type of data to be returned from the database.</typeparam>
        /// <param name="query">The query to be run.</param>
        /// <param name="parameters">The parameters to pass into the query.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="commandTimeout">The command timeout in seconds.</param>
        /// <param name="commandType">The command type.</param>
        /// <returns>An instance of an object of type T.</returns>
        Task<T> QueryFirstAsync<T>(string query, DynamicParameters? parameters = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Queries the database and returns the first result from the result set. Returns null if nothing is returned from the database.
        /// </summary>
        /// <typeparam name="T">The type of data to be returned from the database.</typeparam>
        /// <param name="query">The query to be run.</param>
        /// <param name="parameters">The parameters to pass into the query.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="commandTimeout">The command timeout in seconds.</param>
        /// <param name="commandType">The command type.</param>
        /// <returns>An instance of an object of type T, or null.</returns>
        Task<T> QueryFirstOrDefaultAsync<T>(string query, DynamicParameters? parameters = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        
        /// <summary>
        /// Queries data out of database.
        /// </summary>
        /// <typeparam name="T">The type of data to be returned from the database.</typeparam>
        /// <param name="query">The query to be run.</param>
        /// <param name="parameters">The parameters to pass into the query.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="commandTimeout">The command timeout in seconds.</param>
        /// <param name="commandType">The command type.</param>
        /// <returns>An enumeration of objects of type T.</returns>
        Task<IEnumerable<T>> QueryAsync<T>(string query, DynamicParameters? parameters = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    }
}