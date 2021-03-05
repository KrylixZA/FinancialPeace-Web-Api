using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.ExpenseCategories;
using FinancialPeace.Web.Api.Repositories.Connection;
using Microsoft.Extensions.Logging;

namespace FinancialPeace.Web.Api.Repositories
{
    /// <inheritdoc />
    public class ExpenseCategoriesRepository : IExpenseCategoriesRepository
    {
        private const string GetExpenseCategoriesProc = "Freedom.pr_GetExpenseCategories";
        private const string GetExpenseCategoriesForUserProc = "Freedom.pr_GetExpenseCategoriesForUser";
        private const string CreateExpenseCategoryForUserProc = "Freedom.pr_CreateExpenseCategoryForUser";
        private const string DeleteExpenseCategoryForUserProc = "Freedom.pr_DeleteExpenseCategoryForUser";

        private readonly ISqlConnectionProvider _connectionProvider;
        private readonly ILogger<ExpenseCategoriesRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the Expense Category Repository class.
        /// </summary>
        /// <param name="connectionProvider">The connection provider.</param>
        /// <param name="logger">The logger.</param>
        public ExpenseCategoriesRepository(
            ISqlConnectionProvider connectionProvider,
            ILogger<ExpenseCategoriesRepository> logger)
        {
            _connectionProvider = connectionProvider;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ExpenseCategory>> GetExpenseCategoriesAsync()
        {
            _logger.LogInformation("GetExpenseCategoriesAsync start");
            using var conn = _connectionProvider.Open();
            var response = await conn.QueryAsync<ExpenseCategory>(
                GetExpenseCategoriesProc,
                commandType: CommandType.StoredProcedure);
            _logger.LogInformation("GetExpenseCategoriesAsync end");
            return response;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ExpenseCategory>> GetExpenseCategoriesForUserAsync(Guid userId)
        {
            _logger.LogInformation($"GetExpenseCategoriesForUserAsync start. UserId: {userId}");
            using var conn = _connectionProvider.Open();
            var parameters = new DynamicParameters();
            parameters.Add("$userId", userId);
            var response = await conn.QueryAsync<ExpenseCategory>(
                GetExpenseCategoriesForUserProc,
                parameters,
                commandType: CommandType.StoredProcedure);
            _logger.LogInformation($"GetExpenseCategoriesForUserAsync start. UserId: {userId}");
            return response;
        }

        /// <inheritdoc />
        public async Task AddExpenseCategoryForUserAsync(Guid userId, AddExpenseCategoryRequest request)
        {
            _logger.LogInformation($"AddExpenseCategoryForUserAsync start. UserId: {userId}");
            using var conn = _connectionProvider.Open();
            using var trans = conn.BeginTransaction();
            var parameters = new DynamicParameters();
            parameters.Add("$userId", userId);
            parameters.Add("$expenseCategoryName", request.ExpenseCategoryName);
            await conn.ExecuteNonQueryAsync(
                CreateExpenseCategoryForUserProc,
                parameters,
                trans,
                commandType: CommandType.StoredProcedure);
            trans.Commit();
            _logger.LogInformation($"AddExpenseCategoryForUserAsync end. UserId: {userId}");
        }

        /// <inheritdoc />
        public async Task DeleteExpenseCategoryForUserAsync(Guid userId, Guid expenseCategoryId)
        {
            _logger.LogInformation($"AddExpenseCategoryForUserAsync start. UserId: {userId}. ExpenseCategoryId: {expenseCategoryId}");
            using var conn = _connectionProvider.Open();
            using var trans = conn.BeginTransaction();
            var parameters = new DynamicParameters();
            parameters.Add("$userId", userId);
            parameters.Add("$expenseCategoryId", expenseCategoryId);
            await conn.ExecuteNonQueryAsync(
                DeleteExpenseCategoryForUserProc,
                parameters,
                trans,
                commandType: CommandType.StoredProcedure);
            trans.Commit();
            _logger.LogInformation($"AddExpenseCategoryForUserAsync start. UserId: {userId}. ExpenseCategoryId: {expenseCategoryId}");
        }
    }
}