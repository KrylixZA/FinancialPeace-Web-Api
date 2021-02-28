using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.Budgets;
using FinancialPeace.Web.Api.Repositories.Connection;
using Microsoft.Extensions.Logging;

namespace FinancialPeace.Web.Api.Repositories
{
    /// <inheritdoc />
    public class BudgetsRepository : IBudgetsRepository
    {
        private const string GetBudgetForUserProc = "Freedom.pr_GetBudgetForUser";
        private const string CreateExpenseForUserProc = "Freedom.pr_CreateExpenseForUser";
        private const string DeleteExpenseForUserProc = "Freedom.pr_DeleteExpenseForUser";
        private const string UpdateExpenseForUserProc = "Freedom.pr_UpdateExpenseForUser";

        private readonly ISqlConnectionProvider _connectionProvider;
        private readonly ILogger<BudgetsRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the Budgets Repository class.
        /// </summary>
        /// <param name="connectionProvider">The connection provider.</param>
        /// <param name="logger">The logger.</param>
        public BudgetsRepository(
            ISqlConnectionProvider connectionProvider,
            ILogger<BudgetsRepository> logger)
        {
            _connectionProvider = connectionProvider;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Expense>> GetBudgetForUserAsync(Guid userId)
        {
            _logger.LogInformation($"GetBudgetForUserAsync start. UserId: {userId}");
            using var conn = _connectionProvider.Open();
            var parameters = new DynamicParameters();
            parameters.Add("$userId", userId);
            var response = await conn.QueryAsync<Expense>(
                GetBudgetForUserProc,
                parameters,
                commandType: CommandType.StoredProcedure);
            _logger.LogInformation($"GetBudgetForUserAsync end. UserId: {userId}");
            return response;
        }

        /// <inheritdoc />
        public async Task CreateExpenseForUserAsync(Guid userId, CreateExpenseRequest request)
        {
            _logger.LogInformation($"CreateExpenseForUserAsync start. UserId: {userId}");
            using var conn = _connectionProvider.Open();
            using var trans = conn.BeginTransaction();
            var parameters = new DynamicParameters();
            parameters.Add("$userId", userId);
            parameters.Add("$expenseCategoryName", request.ExpenseCategoryName);
            parameters.Add("$countryCurrencyCode", request.CountryCurrencyCode);
            parameters.Add("$value", request.Value);
            await conn.ExecuteNonQueryAsync(
                CreateExpenseForUserProc,
                parameters,
                trans,
                commandType: CommandType.StoredProcedure);
            trans.Commit();
            _logger.LogInformation($"CreateExpenseForUserAsync end. UserId: {userId}");
        }

        /// <inheritdoc />
        public async Task DeleteExpenseForUserAsync(Guid userId, Guid expenseId)
        {
            _logger.LogInformation($"DeleteExpenseForUserAsync start. UserId: {userId}. ExpenseId: {expenseId}");
            using var conn = _connectionProvider.Open();
            using var trans = conn.BeginTransaction();
            var parameters = new DynamicParameters();
            parameters.Add("$expenseId", expenseId);
            parameters.Add("$userId", userId);
            await conn.ExecuteNonQueryAsync(
                DeleteExpenseForUserProc,
                parameters,
                trans,
                commandType: CommandType.StoredProcedure);
            trans.Commit();
            _logger.LogInformation($"DeleteExpenseForUserAsync end. UserId: {userId}. ExpenseId: {expenseId}");
        }

        /// <inheritdoc />
        public async Task UpdateExpenseForUserAsync(Guid userId, Guid expenseId, UpdateExpenseRequest request)
        {
            _logger.LogInformation($"UpdateExpenseForUserAsync start. UserId: {userId}. ExpenseId: {expenseId}");
            using var conn = _connectionProvider.Open();
            using var trans = conn.BeginTransaction();
            var parameters = new DynamicParameters();
            parameters.Add("$expenseId", expenseId);
            parameters.Add("$userId", userId);
            parameters.Add("$expenseCategoryName", request.ExpenseCategoryName);
            parameters.Add("$countryCurrencyCode", request.CountryCurrencyCode);
            parameters.Add("$value", request.Value);
            await conn.ExecuteNonQueryAsync(
                UpdateExpenseForUserProc,
                parameters,
                trans,
                commandType: CommandType.StoredProcedure);
            trans.Commit();
            _logger.LogInformation($"UpdateExpenseForUserAsync start. UserId: {userId}. ExpenseId: {expenseId}");
        }
    }
}