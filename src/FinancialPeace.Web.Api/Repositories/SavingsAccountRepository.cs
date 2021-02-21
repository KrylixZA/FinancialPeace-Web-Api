using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.SavingsAccounts;
using FinancialPeace.Web.Api.Repositories.Connection;

namespace FinancialPeace.Web.Api.Repositories
{
    /// <inheritdoc />
    public class SavingsAccountRepository : ISavingsAccountRepository
    {
        private const string GetSavingsAccountForUserProc = "Freedom.pr_GetSavingsAccountsForUser";
        private const string CreateSavingsAccountForUserProc = "Freedom.pr_CreateSavingsAccountForUser";
        private const string AddAmountToSavingsAccountForUserProc = "Freedom.pr_AddAmountToSavingsAccountForUser";
        private const string SubtractAmountFromSavingsAccountForUserProc = "Freedom.pr_SubtractAmountFromSavingsAccountForUser";
        private const string DeleteSavingsAccountForUserProc = "Freedom.pr_DeleteSavingsAccountForUser";
        private const string UpdateSavingsAccountForUserProc = "Freedom.pr_UpdateSavingsAccountForUser";
        
        private readonly ISqlConnectionProvider _sqlConnectionProvider;

        /// <summary>
        /// Initializes a new instance of the Savings Account Repository class.
        /// </summary>
        /// <param name="sqlConnectionProvider"></param>
        public SavingsAccountRepository(ISqlConnectionProvider sqlConnectionProvider)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<SavingsAccount>> GetSavingsAccountForUser(Guid userId)
        {
            using var conn = _sqlConnectionProvider.Open();
            var parameters = new DynamicParameters();
            parameters.Add("$userId", userId);
            return await conn.QueryAsync<SavingsAccount>(
                GetSavingsAccountForUserProc,
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        /// <inheritdoc />
        public async Task AddSavingsAccountForUser(Guid userId, AddSavingsAccountRequest request)
        {
            using var conn = _sqlConnectionProvider.Open();
            using var trans = conn.BeginTransaction();
            var parameters = new DynamicParameters();
            parameters.Add("$userId", userId);
            parameters.Add("$countryCurrencyCode", request.CountryCurrencyCode);
            parameters.Add("$savingsValue", request.SavingsValue);
            parameters.Add("$savingsTarget", request.SavingsTarget);
            parameters.Add("$name", request.Name);
            await conn.ExecuteNonQueryAsync(
                CreateSavingsAccountForUserProc,
                parameters,
                trans,
                commandType: CommandType.StoredProcedure);
            trans.Commit();
        }

        /// <inheritdoc />
        public async Task AddAmountToSavingsAccountForUser(Guid userId, Guid savingsAccountId, AddAmountToSavingsAccountRequest request)
        {
            using var conn = _sqlConnectionProvider.Open();
            using var trans = conn.BeginTransaction();
            var parameters = new DynamicParameters();
            parameters.Add("$savingsAccountId", savingsAccountId);
            parameters.Add("$userId", userId);
            parameters.Add("$amount", request.Amount);
            await conn.ExecuteNonQueryAsync(
                AddAmountToSavingsAccountForUserProc,
                parameters,
                trans,
                commandType: CommandType.StoredProcedure);
            trans.Commit();
        }

        /// <inheritdoc />
        public async Task SubtractAmountFromSavingsAccountForUser(Guid userId, Guid savingsAccountId, SubtractAmountFromSavingsAccountRequest request)
        {
            using var conn = _sqlConnectionProvider.Open();
            using var trans = conn.BeginTransaction();
            var parameters = new DynamicParameters();
            parameters.Add("$savingsAccountId", savingsAccountId);
            parameters.Add("$userId", userId);
            parameters.Add("$amount", request.Amount);
            await conn.ExecuteNonQueryAsync(
                SubtractAmountFromSavingsAccountForUserProc,
                parameters,
                trans,
                commandType: CommandType.StoredProcedure);
            trans.Commit();
        }

        /// <inheritdoc />
        public async Task DeleteSavingsAccountForUser(Guid userId, Guid savingsAccountId)
        {
            using var conn = _sqlConnectionProvider.Open();
            using var trans = conn.BeginTransaction();
            var parameters = new DynamicParameters();
            parameters.Add("$savingsAccountId", savingsAccountId);
            parameters.Add("$userId", userId);
            await conn.ExecuteNonQueryAsync(
                DeleteSavingsAccountForUserProc,
                parameters,
                trans,
                commandType: CommandType.StoredProcedure);
            trans.Commit();
        }

        /// <inheritdoc />
        public async Task UpdateSavingsAccountForUser(Guid userId, Guid savingsAccountId, UpdateSavingsAccountRequest request)
        {
            using var conn = _sqlConnectionProvider.Open();
            using var trans = conn.BeginTransaction();
            var parameters = new DynamicParameters();
            parameters.Add("$savingsAccountId", savingsAccountId);
            parameters.Add("$userId", userId);
            parameters.Add("$countryCurrencyCode", request.CountryCurrencyCode);
            parameters.Add("$currentSavingsAmount", request.CurrentSavingsAmount);
            parameters.Add("$savingsTarget", request.TargetSavingsAmount);
            parameters.Add("$name", request.Name);
            await conn.ExecuteNonQueryAsync(
                UpdateSavingsAccountForUserProc,
                parameters,
                trans,
                commandType: CommandType.StoredProcedure);
            trans.Commit();
        }
    }
}