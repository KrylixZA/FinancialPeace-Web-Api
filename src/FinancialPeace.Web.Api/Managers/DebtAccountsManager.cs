using System;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Models.Requests.DebtAccounts;
using FinancialPeace.Web.Api.Models.Responses.DebtAccounts;
using FinancialPeace.Web.Api.Repositories;
using Microsoft.Extensions.Logging;

namespace FinancialPeace.Web.Api.Managers
{
    /// <inheritdoc />
    public class DebtAccountsManager : IDebtAccountsManager
    {
        private readonly IDebtAccountsRepository _debtAccountsRepository;
        private readonly ILogger<DebtAccountsManager> _logger;

        /// <summary>
        /// Creates a new instance of the Debt Account Manager class.
        /// </summary>
        /// <param name="debtAccountsRepository">The debt accounts repository.</param>
        /// <param name="logger">The logger.</param>
        public DebtAccountsManager(
            IDebtAccountsRepository debtAccountsRepository,
            ILogger<DebtAccountsManager> logger)
        {
            _debtAccountsRepository = debtAccountsRepository;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<GetDebtAccountsForUserResponse> GetDebtAccountForUserAsync(Guid userId)
        {
            _logger.LogInformation($"GetDebtAccountForUserAsync start. UserId: {userId}");
            var debtAccounts = await _debtAccountsRepository.GetDebtAccountsForUserAsync(userId);
            _logger.LogInformation($"GetDebtAccountForUserAsync end. UserId: {userId}");
            return new GetDebtAccountsForUserResponse
            {
                DebtAccounts = debtAccounts,
                UserId = userId
            };
        }

        /// <inheritdoc />
        public Task AddDebtAccountForUserAsync(Guid userId, AddDebtAccountRequest request)
        {
            _logger.LogInformation($"AddDebtAccountForUser start. UserId: {userId}");
            var responseTask = _debtAccountsRepository.AddDebtAccountForUserAsync(userId, request);
            _logger.LogInformation($"AddDebtAccountForUser end. UserId: {userId}");
            return responseTask;
        }

        /// <inheritdoc />
        public Task AddAmountToDebtAccountForUserAsync(
            Guid userId, 
            Guid debtAccountId,
            AddAmountToDebtAccountRequest request)
        {
            _logger.LogInformation($"AddAmountToDebtAccountForUserAsync start. UserId: {userId}. DebtAccountId: {debtAccountId}");
            var responseTask = _debtAccountsRepository.AddAmountToDebtAccountForUserAsync(userId, debtAccountId, request);
            _logger.LogInformation($"AddAmountToDebtAccountForUserAsync end. UserId: {userId}. DebtAccountId: {debtAccountId}");
            return responseTask;
        }

        /// <inheritdoc />
        public Task SubtractAmountFromDebtAccountForUserAsync(
            Guid userId, 
            Guid debtAccountId,
            SubtractAmountFromDebtAccountRequest request)
        {
            _logger.LogInformation($"SubtractAmountFromDebtAccountForUserAsync start. UserId: {userId}. DebtAccountId: {debtAccountId}");
            var responseTask = _debtAccountsRepository.SubtractAmountFromDebtAccountForUserAsync(userId, debtAccountId, request);
            _logger.LogInformation($"SubtractAmountFromDebtAccountForUserAsync end. UserId: {userId}. DebtAccountId: {debtAccountId}");
            return responseTask;
        }

        /// <inheritdoc />
        public Task DeleteDebtAccountForUserAsync(Guid userId, Guid debtAccountId)
        {
            _logger.LogInformation($"DeleteDebtAccountForUserAsync start. UserId: {userId}. DebtAccountId: {debtAccountId}");
            var responseTask = _debtAccountsRepository.DeleteDebtAccountForUserAsync(userId, debtAccountId);
            _logger.LogInformation($"DeleteDebtAccountForUserAsync end. UserId: {userId}. DebtAccountId: {debtAccountId}");
            return responseTask;
        }

        /// <inheritdoc />
        public Task UpdateDebtAccountForUserAsync(Guid userId, Guid debtAccountId, UpdateDebtAccountRequest request)
        {
            _logger.LogInformation($"UpdateDebtAccountForUserAsync start. UserId: {userId}. DebtAccountId: {debtAccountId}");
            var responseTask = _debtAccountsRepository.UpdateDebtAccountForUserAsync(userId, debtAccountId, request);
            _logger.LogInformation($"UpdateDebtAccountForUserAsync end. UserId: {userId}. DebtAccountId: {debtAccountId}");
            return responseTask;
        }
    }
}