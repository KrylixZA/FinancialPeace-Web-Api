using System;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Models.Requests.SavingsAccounts;
using FinancialPeace.Web.Api.Models.Responses.SavingsAccounts;

namespace FinancialPeace.Web.Api.Managers
{
    /// <summary>
    /// Provides a contract for managing a user's savings accounts.
    /// </summary>
    public interface ISavingsAccountManager
    {
        /// <summary>
        /// Gets the list of savings accounts linked to the user from the database.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <returns>An enumeration of savings account.</returns>
        Task<GetSavingsAccountForUserResponse> GetSavingsAccountForUser(Guid userId);
        
        /// <summary>
        /// Creates a new savings account for the user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="request">The details of the savings account.</param>
        Task AddSavingsAccountForUser(Guid userId, AddSavingsAccountRequest request);

        /// <summary>
        /// Adds an amount to the user's savings account.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="savingsAccountId">The savings account unique identifier.</param>
        /// <param name="request">The details of the amount to add.</param>
        Task AddAmountToSavingsAccountForUser(Guid userId, Guid savingsAccountId, AddAmountToSavingsAccountRequest request);
        
        /// <summary>
        /// Subtracts an amount from a user's savings account.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="savingsAccountId">The savings account unique identifier.</param>
        /// <param name="request">The details of the amount to subtract.</param>
        Task SubtractAmountFromSavingsAccountForUser(Guid userId, Guid savingsAccountId, SubtractAmountFromSavingsAccountRequest request);

        /// <summary>
        /// Deletes a savings account for a user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="savingsAccountId">The savings account unique identifier.</param>
        Task DeleteSavingsAccountForUser(Guid userId, Guid savingsAccountId);

        /// <summary>
        /// Updates a user's savings account.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="savingsAccountId">The savings account unique identifier.</param>
        /// <param name="request">The details of the update.</param>
        Task UpdateSavingsAccountForUser(Guid userId, Guid savingsAccountId, UpdateSavingsAccountRequest request);
    }
}