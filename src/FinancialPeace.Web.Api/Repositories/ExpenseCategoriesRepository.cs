using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.ExpenseCategories;
using FinancialPeace.Web.Api.Repositories.Connection;

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

        /// <summary>
        /// Initializes a new instance of the Expense Category Repository class.
        /// </summary>
        /// <param name="connectionProvider">The connection provider.</param>
        public ExpenseCategoriesRepository(ISqlConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ExpenseCategory>> GetExpenseCategories()
        {
            using var conn = _connectionProvider.Open();
            return await conn.QueryAsync<ExpenseCategory>(
                GetExpenseCategoriesProc,
                commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ExpenseCategory>> GetExpenseCategoriesForUser(Guid userId)
        {
            using var conn = _connectionProvider.Open();
            var parameters = new DynamicParameters();
            parameters.Add("$userId", userId);
            return await conn.QueryAsync<ExpenseCategory>(
                GetExpenseCategoriesForUserProc,
                parameters,
                commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task AddExpenseCategoryForUser(Guid userId, AddExpenseCategoryRequest request)
        {
            using var conn = _connectionProvider.Open();
            using var trans = conn.BeginTransaction();
            var parameters = new DynamicParameters();
            parameters.Add("$userId", userId);
            parameters.Add("$expenseCategoryName", request.ExpenseCategoryName);
            await conn.ExecuteNonQueryAsync(
                CreateExpenseCategoryForUserProc,
                parameters,
                trans,
                commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            trans.Commit();
        }

        /// <inheritdoc />
        public async Task DeleteExpenseCategoryForUser(Guid userId, Guid expenseCategoryId)
        {
            using var conn = _connectionProvider.Open();
            using var trans = conn.BeginTransaction();
            var parameters = new DynamicParameters();
            parameters.Add("$userId", userId);
            parameters.Add("$expenseCategoryId", expenseCategoryId);
            await conn.ExecuteNonQueryAsync(
                DeleteExpenseCategoryForUserProc,
                parameters,
                trans,
                commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            trans.Commit();
        }
    }
}