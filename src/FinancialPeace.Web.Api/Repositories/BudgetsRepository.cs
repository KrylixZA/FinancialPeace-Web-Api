using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.Budgets;
using FinancialPeace.Web.Api.Repositories.Connection;

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

        /// <summary>
        /// Initializes a new instance of the Budgets Repository class.
        /// </summary>
        /// <param name="connectionProvider">The connection provider.</param>
        public BudgetsRepository(ISqlConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider ?? throw new ArgumentNullException(nameof(connectionProvider));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Expense>> GetBudgetForUserAsync(Guid userId)
        {
            using var conn = _connectionProvider.Open();
            var parameters = new DynamicParameters();
            parameters.Add("$userId", userId);
            return await conn.QueryAsync<Expense>(
                GetBudgetForUserProc,
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        /// <inheritdoc />
        public async Task CreateExpenseForUserAsync(Guid userId, CreateExpenseRequest request)
        {
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
        }

        /// <inheritdoc />
        public async Task DeleteExpenseForUserAsync(Guid userId, Guid expenseId)
        {
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
        }

        /// <inheritdoc />
        public async Task UpdateExpenseForUserAsync(Guid userId, Guid expenseId, UpdateExpenseRequest request)
        {
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
        }
    }
}