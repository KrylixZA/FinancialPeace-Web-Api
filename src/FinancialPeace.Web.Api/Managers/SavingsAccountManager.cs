using System;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Models.Requests.SavingsAccounts;
using FinancialPeace.Web.Api.Models.Responses.SavingsAccounts;
using FinancialPeace.Web.Api.Repositories;

namespace FinancialPeace.Web.Api.Managers
{
    /// <inheritdoc />
    public class SavingsAccountManager : ISavingsAccountManager
    {
        private readonly ISavingsAccountRepository _savingsAccountRepository;

        /// <summary>
        /// Initializes a new instance of the Savings Account Manager class.
        /// </summary>
        /// <param name="savingsAccountRepository">The savings account repository.</param>
        public SavingsAccountManager(ISavingsAccountRepository savingsAccountRepository)
        {
            _savingsAccountRepository = savingsAccountRepository ?? throw new ArgumentNullException(nameof(savingsAccountRepository));
        }

        /// <inheritdoc />
        public async Task<GetSavingsAccountForUserResponse> GetSavingsAccountForUser(Guid userId)
        {
            var accounts = await _savingsAccountRepository.GetSavingsAccountForUser(userId);
            return new GetSavingsAccountForUserResponse
            {
                SavingsAccounts = accounts,
                UserId = userId
            };
        }

        /// <inheritdoc />
        public Task AddSavingsAccountForUser(Guid userId, AddSavingsAccountRequest request)
        {
            return _savingsAccountRepository.AddSavingsAccountForUser(userId, request);
        }

        /// <inheritdoc />
        public Task AddAmountToSavingsAccountForUser(Guid userId, Guid savingsAccountId, AddAmountToSavingsAccountRequest request)
        {
            return _savingsAccountRepository.AddAmountToSavingsAccountForUser(userId, savingsAccountId, request);
        }

        /// <inheritdoc />
        public Task SubtractAmountFromSavingsAccountForUser(Guid userId, Guid savingsAccountId, SubtractAmountFromSavingsAccountRequest request)
        {
            return _savingsAccountRepository.SubtractAmountFromSavingsAccountForUser(userId, savingsAccountId, request);
        }

        /// <inheritdoc />
        public Task DeleteSavingsAccountForUser(Guid userId, Guid savingsAccountId)
        {
            return _savingsAccountRepository.DeleteSavingsAccountForUser(userId, savingsAccountId);
        }

        /// <inheritdoc />
        public Task UpdateSavingsAccountForUser(Guid userId, Guid savingsAccountId, UpdateSavingsAccountRequest request)
        {
            return _savingsAccountRepository.UpdateSavingsAccountForUser(userId, savingsAccountId, request);
        }
    }
}