using System;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Models.Requests.DebtAccounts;
using FinancialPeace.Web.Api.Models.Responses.DebtAccounts;

namespace FinancialPeace.Web.Api.Managers
{
    /// <summary>
    /// Provides a contract for managing a user's debt accounts.
    /// </summary>
    public interface IDebtAccountsManager
    {
        /// <summary>
        /// Gets the list of debt accounts linked to the user from the database.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <returns>An enumeration of debt accounts.</returns>
        Task<GetDebtAccountsForUserResponse> GetDebtAccountForUserAsync(Guid userId);
        
        /// <summary>
        /// Creates a new debt account for the user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="request">The details of the debt account.</param>
        Task AddDebtAccountForUserAsync(Guid userId, AddDebtAccountRequest request);

        /// <summary>
        /// Adds an amount to the user's debt account.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="debtAccountId">The savings account unique identifier.</param>
        /// <param name="request">The details of the amount to add.</param>
        Task AddAmountToDebtAccountForUserAsync(Guid userId, Guid debtAccountId, AddAmountToDebtAccountRequest request);
        
        /// <summary>
        /// Subtracts an amount from a user's debt account.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="debtAccountId">The debt account unique identifier.</param>
        /// <param name="request">The details of the amount to subtract.</param>
        Task SubtractAmountFromDebtAccountForUserAsync(Guid userId, Guid debtAccountId, SubtractAmountFromDebtAccountRequest request);

        /// <summary>
        /// Deletes a debt account for a user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="debtAccountId">The debt account unique identifier.</param>
        Task DeleteDebtAccountForUserAsync(Guid userId, Guid debtAccountId);

        /// <summary>
        /// Updates a user's debt account.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="debtAccountId">The debt account unique identifier.</param>
        /// <param name="request">The details of the update.</param>
        Task UpdateDebtAccountForUserAsync(Guid userId, Guid debtAccountId, UpdateDebtAccountRequest request);
    }
}