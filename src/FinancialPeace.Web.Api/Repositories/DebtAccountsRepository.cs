using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.DebtAccounts;
using FinancialPeace.Web.Api.Repositories.Connection;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<DebtAccountsRepository> _logger;

        /// <summary>
        /// Creates a new instance of the Debt Account Repository class.
        /// </summary>
        /// <param name="sqlConnectionProvider">The SQL connection provider.</param>
        /// <param name="logger">The logger.</param>
        public DebtAccountsRepository(
            ISqlConnectionProvider sqlConnectionProvider,
            ILogger<DebtAccountsRepository> logger)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<DebtAccount>> GetDebtAccountsForUserAsync(Guid userId)
        {
            _logger.LogInformation($"GetDebtAccountsForUserAsync start. UserId: {userId}");
            using var conn = _sqlConnectionProvider.Open();
            var parameters = new DynamicParameters();
            parameters.Add("$userId", userId);
            var response = await conn.QueryAsync<DebtAccount>(
                GetDebtsAccountForUserProc, 
                parameters,
                commandType: CommandType.StoredProcedure);
            _logger.LogInformation($"GetDebtAccountsForUserAsync end. UserId: {userId}");
            return response;
        }

        /// <inheritdoc />
        public async Task AddDebtAccountForUserAsync(Guid userId, AddDebtAccountRequest request)
        {
            _logger.LogInformation($"AddDebtAccountForUserAsync start. UserId: {userId}");
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
            _logger.LogInformation($"AddDebtAccountForUserAsync end. UserId: {userId}");
        }

        /// <inheritdoc />
        public async Task AddAmountToDebtAccountForUserAsync(
            Guid userId, 
            Guid debtAccountId, 
            AddAmountToDebtAccountRequest request)
        {
            _logger.LogInformation($"AddAmountToDebtAccountForUserAsync start. UserId: {userId}. DebtAccountId: {debtAccountId}");
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
            _logger.LogInformation($"AddAmountToDebtAccountForUserAsync end. UserId: {userId}. DebtAccountId: {debtAccountId}");
        }

        /// <inheritdoc />
        public async Task SubtractAmountFromDebtAccountForUserAsync(
            Guid userId, 
            Guid debtAccountId, 
            SubtractAmountFromDebtAccountRequest request)
        {
            _logger.LogInformation($"SubtractAmountFromDebtAccountForUserAsync start. UserId: {userId}. DebtAccountId: {debtAccountId}");
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
            _logger.LogInformation($"SubtractAmountFromDebtAccountForUserAsync start. UserId: {userId}. DebtAccountId: {debtAccountId}");
        }

        /// <inheritdoc />
        public async Task DeleteDebtAccountForUserAsync(Guid userId, Guid debtAccountId)
        {
            _logger.LogInformation($"DeleteDebtAccountForUserAsync start. UserId: {userId}. DebtAccountId: {debtAccountId}");
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
            _logger.LogInformation($"DeleteDebtAccountForUserAsync start. UserId: {userId}. DebtAccountId: {debtAccountId}");
        }

        /// <inheritdoc />
        public async Task UpdateDebtAccountForUserAsync(
            Guid userId, 
            Guid debtAccountId, 
            UpdateDebtAccountRequest request)
        {
            _logger.LogInformation($"UpdateDebtAccountForUserAsync start. UserId: {userId}. DebtAccountId: {debtAccountId}");
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
            _logger.LogInformation($"UpdateDebtAccountForUserAsync end. UserId: {userId}. DebtAccountId: {debtAccountId}");
        }
    }
}