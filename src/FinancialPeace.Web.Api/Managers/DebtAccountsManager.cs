using System;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Models.Requests.DebtAccounts;
using FinancialPeace.Web.Api.Models.Responses.DebtAccounts;
using FinancialPeace.Web.Api.Repositories;

namespace FinancialPeace.Web.Api.Managers
{
    /// <inheritdoc />
    public class DebtAccountsManager : IDebtAccountsManager
    {
        private readonly IDebtAccountsRepository _debtAccountsRepository;

        /// <summary>
        /// Creates a new instance of the Debt Account Manager class.
        /// </summary>
        /// <param name="debtAccountsRepository">The debt accounts repository.</param>
        public DebtAccountsManager(IDebtAccountsRepository debtAccountsRepository)
        {
            _debtAccountsRepository =
                debtAccountsRepository ?? throw new ArgumentNullException(nameof(debtAccountsRepository));
        }

        /// <inheritdoc />
        public async Task<GetDebtAccountsForUserResponse> GetDebtAccountForUser(Guid userId)
        {
            var debtAccounts = await _debtAccountsRepository.GetDebtAccountsForUser(userId);
            return new GetDebtAccountsForUserResponse
            {
                DebtAccounts = debtAccounts,
                UserId = userId
            };
        }

        /// <inheritdoc />
        public Task AddDebtAccountForUser(Guid userId, AddDebtAccountRequest request)
        {
            return _debtAccountsRepository.AddDebtAccountForUser(userId, request);
        }

        /// <inheritdoc />
        public Task AddAmountToDebtAccountForUser(Guid userId, Guid debtAccountId, AddAmountToDebtAccountRequest request)
        {
            return _debtAccountsRepository.AddAmountToDebtAccountForUser(userId, debtAccountId, request);
        }

        /// <inheritdoc />
        public Task SubtractAmountFromDebtAccountForUser(Guid userId, Guid debtAccountId, SubtractAmountFromDebtAccountRequest request)
        {
            return _debtAccountsRepository.SubtractAmountFromDebtAccountForUser(userId, debtAccountId, request);
        }

        /// <inheritdoc />
        public Task DeleteDebtAccountForUser(Guid userId, Guid debtAccountId)
        {
            return _debtAccountsRepository.DeleteDebtAccountForUser(userId, debtAccountId);
        }

        /// <inheritdoc />
        public Task UpdateDebtAccountForUser(Guid userId, Guid debtAccountId, UpdateDebtAccountRequest request)
        {
            return _debtAccountsRepository.UpdateDebtAccountForUser(userId, debtAccountId, request);
        }
    }
}