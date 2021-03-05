using System;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Models.Requests.SavingsAccounts;
using FinancialPeace.Web.Api.Models.Responses.SavingsAccounts;
using FinancialPeace.Web.Api.Repositories;
using Microsoft.Extensions.Logging;

namespace FinancialPeace.Web.Api.Managers
{
    /// <inheritdoc />
    public class SavingsAccountManager : ISavingsAccountManager
    {
        private readonly ISavingsAccountRepository _savingsAccountRepository;
        private readonly ILogger<SavingsAccountManager> _logger;

        /// <summary>
        /// Initializes a new instance of the Savings Account Manager class.
        /// </summary>
        /// <param name="savingsAccountRepository">The savings account repository.</param>
        /// <param name="logger">The logger.</param>
        public SavingsAccountManager(
            ISavingsAccountRepository savingsAccountRepository,
            ILogger<SavingsAccountManager> logger)
        {
            _savingsAccountRepository = savingsAccountRepository;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<GetSavingsAccountForUserResponse> GetSavingsAccountForUserAsync(Guid userId)
        {
            _logger.LogInformation($"GetSavingsAccountForUserAsync start. UserId: {userId}");
            var accounts = await _savingsAccountRepository.GetSavingsAccountForUserAsync(userId);
            _logger.LogInformation($"GetSavingsAccountForUserAsync end. UserId: {userId}");
            return new GetSavingsAccountForUserResponse
            {
                SavingsAccounts = accounts,
                UserId = userId
            };
        }

        /// <inheritdoc />
        public Task AddSavingsAccountForUserAsync(Guid userId, AddSavingsAccountRequest request)
        {
            _logger.LogInformation($"AddSavingsAccountForUserAsync start. UserId: {userId}");
            var responseTask = _savingsAccountRepository.AddSavingsAccountForUserAsync(userId, request);
            _logger.LogInformation($"AddSavingsAccountForUserAsync end. UserId: {userId}");
            return responseTask;
        }

        /// <inheritdoc />
        public Task AddAmountToSavingsAccountForUserAsync(Guid userId, Guid savingsAccountId, AddAmountToSavingsAccountRequest request)
        {
            _logger.LogInformation($"AddAmountToSavingsAccountForUserAsync start. UserId: {userId}. AccountId: {savingsAccountId}");
            var responseTask = _savingsAccountRepository.AddAmountToSavingsAccountForUserAsync(userId, savingsAccountId, request);
            _logger.LogInformation($"AddAmountToSavingsAccountForUserAsync end. UserId: {userId}. AccountId: {savingsAccountId}");
            return responseTask;
        }

        /// <inheritdoc />
        public Task SubtractAmountFromSavingsAccountForUserAsync(Guid userId, Guid savingsAccountId, SubtractAmountFromSavingsAccountRequest request)
        {
            _logger.LogInformation($"SubtractAmountFromSavingsAccountForUserAsync start. UserId: {userId}. AccountId: {savingsAccountId}");
            var responseTask = _savingsAccountRepository.SubtractAmountFromSavingsAccountForUserAsync(userId, savingsAccountId, request);
            _logger.LogInformation($"SubtractAmountFromSavingsAccountForUserAsync end. UserId: {userId}. AccountId: {savingsAccountId}");
            return responseTask;
        }

        /// <inheritdoc />
        public Task DeleteSavingsAccountForUserAsync(Guid userId, Guid savingsAccountId)
        {
            _logger.LogInformation($"DeleteSavingsAccountForUserAsync start. UserId: {userId}. AccountId: {savingsAccountId}");
            var responseTask = _savingsAccountRepository.DeleteSavingsAccountForUserAsync(userId, savingsAccountId);
            _logger.LogInformation($"DeleteSavingsAccountForUserAsync end. UserId: {userId}. AccountId: {savingsAccountId}");
            return responseTask;
        }

        /// <inheritdoc />
        public Task UpdateSavingsAccountForUserAsync(Guid userId, Guid savingsAccountId, UpdateSavingsAccountRequest request)
        {
            _logger.LogInformation($"UpdateSavingsAccountForUserAsync start. UserId: {userId}. AccountId: {savingsAccountId}");
            var responseTask = _savingsAccountRepository.UpdateSavingsAccountForUserAsync(userId, savingsAccountId, request);
            _logger.LogInformation($"UpdateSavingsAccountForUserAsync end. UserId: {userId}. AccountId: {savingsAccountId}");
            return responseTask;
        }
    }
}