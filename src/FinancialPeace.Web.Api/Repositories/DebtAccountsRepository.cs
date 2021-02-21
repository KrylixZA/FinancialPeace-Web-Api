using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.DebtAccounts;
using FinancialPeace.Web.Api.Repositories.Connection;

namespace FinancialPeace.Web.Api.Repositories
{
    /// <inheritdoc />
    public class DebtAccountsRepository : IDebtAccountsRepository
    {
        private const string GetDebtsAccountForUserProc = "Freedom.pr_GetDebtAccountsForUser";
        private const string AddDebtAccountForUserProc = "Freedom.pr_CreateDebtAccountForUser";
        private const string AddAmountToDebtAccountForUserProc = "Freedom.pr_AddAmountToDebtAccountForUser";
        private const string SubtractAmountFromDebtAccountForUserProc = "Freedom.pr_SubtractAmountFromDebtAccountForUser";
        private const string DeleteDebtAccountForUserProc = "Freedom.pr_DeleteDebtAccountForUser";
        private const string UpdateDebtAccountForUserProc = "Freedom.pr_UpdateDebtAccountForUser";
        
        private readonly ISqlConnectionProvider _sqlConnectionProvider;

        /// <summary>
        /// Creates a new instance of the Debt Account Repository class.
        /// </summary>
        /// <param name="sqlConnectionProvider">The SQL connection provider.</param>
        public DebtAccountsRepository(ISqlConnectionProvider sqlConnectionProvider)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<DebtAccount>> GetDebtAccountsForUser(Guid userId)
        {
            using var conn = _sqlConnectionProvider.Open();
            var parameters = new DynamicParameters();
            parameters.Add("$userId", userId);
            return await conn.QueryAsync<DebtAccount>(
                GetDebtsAccountForUserProc, 
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        /// <inheritdoc />
        public async Task AddDebtAccountForUser(Guid userId, AddDebtAccountRequest request)
        {
            using var conn = _sqlConnectionProvider.Open();
            using var trans = conn.BeginTransaction();
            var parameters = new DynamicParameters();
            parameters.Add("$userId", userId);
            parameters.Add("$countryCurrencyCode", request.CountryCurrencyCode);
            parameters.Add("$amountOwed", request.AmountOwed);
            parameters.Add("$targetPayoffDate", request.TargetPayoffDate);
            parameters.Add("$name", request.Name);
            await conn.ExecuteNonQueryAsync(
                AddDebtAccountForUserProc,
                parameters,
                trans,
                commandType: CommandType.StoredProcedure);
            trans.Commit();
        }

        /// <inheritdoc />
        public async Task AddAmountToDebtAccountForUser(Guid userId, Guid debtAccountId, AddAmountToDebtAccountRequest request)
        {
            using var conn = _sqlConnectionProvider.Open();
            using var trans = conn.BeginTransaction();
            var parameters = new DynamicParameters();
            parameters.Add("$debtAccountId", debtAccountId);
            parameters.Add("$userId", userId);
            parameters.Add("$amount", request.Amount);
            await conn.ExecuteNonQueryAsync(
                AddAmountToDebtAccountForUserProc,
                parameters,
                trans,
                commandType: CommandType.StoredProcedure);
            trans.Commit();
        }

        /// <inheritdoc />
        public async Task SubtractAmountFromDebtAccountForUser(Guid userId, Guid debtAccountId, SubtractAmountFromDebtAccountRequest request)
        {
            using var conn = _sqlConnectionProvider.Open();
            using var trans = conn.BeginTransaction();
            var parameters = new DynamicParameters();
            parameters.Add("$debtAccountId", debtAccountId);
            parameters.Add("$userId", userId);
            parameters.Add("$amount", request.Amount);
            await conn.ExecuteNonQueryAsync(
                SubtractAmountFromDebtAccountForUserProc,
                parameters,
                trans,
                commandType: CommandType.StoredProcedure);
            trans.Commit();
        }

        /// <inheritdoc />
        public async Task DeleteDebtAccountForUser(Guid userId, Guid debtAccountId)
        {
            using var conn = _sqlConnectionProvider.Open();
            using var trans = conn.BeginTransaction();
            var parameters = new DynamicParameters();
            parameters.Add("$debtAccountId", debtAccountId);
            parameters.Add("$userId", userId);
            await conn.ExecuteNonQueryAsync(
                DeleteDebtAccountForUserProc,
                parameters,
                trans,
                commandType: CommandType.StoredProcedure);
            trans.Commit();
        }

        /// <inheritdoc />
        public async Task UpdateDebtAccountForUser(Guid userId, Guid debtAccountId, UpdateDebtAccountRequest request)
        {
            using var conn = _sqlConnectionProvider.Open();
            using var trans = conn.BeginTransaction();
            var parameters = new DynamicParameters();
            parameters.Add("$debtAccountId", debtAccountId);
            parameters.Add("$userId", userId);
            parameters.Add("$countryCurrencyCode", request.CountryCurrencyCode);
            parameters.Add("$currentAmountOwed", request.CurrentAmountOwed);
            parameters.Add("$targetPayoffDate", request.TargetPayoffDate);
            parameters.Add("$actualPayoffDate", request.ActualPayoffDate);
            parameters.Add("$name", request.Name);
            await conn.ExecuteNonQueryAsync(
                UpdateDebtAccountForUserProc,
                parameters,
                trans,
                commandType: CommandType.StoredProcedure);
            trans.Commit();
        }
    }
}