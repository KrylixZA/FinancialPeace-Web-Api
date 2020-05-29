using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.DebtAccounts;

namespace FinancialPeace.Web.Api.Repositories
{
    /// <summary>
    /// Provides a contract for interacting with a user's debt accounts in the database.
    /// </summary>
    public interface IDebtAccountsRepository
    {
        /// <summary>
        /// Gets the list of debt accounts linked to the user from the database.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <returns>An enumeration of debt account.</returns>
        Task<IEnumerable<DebtAccount>> GetDebtAccountsForUser(Guid userId);

        /// <summary>
        /// Creates a new savings account for the user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="request">The details of the debt account.</param>
        Task AddDebtAccountForUser(Guid userId, AddDebtAccountRequest request);

        /// <summary>
        /// Adds an amount to the user's savings account.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="debtAccountId">The debt account unique identifier.</param>
        /// <param name="request">The details of the amount to add.</param>
        Task AddAmountToDebtAccountForUser(Guid userId, Guid debtAccountId, AddAmountToDebtAccountRequest request);
        
        /// <summary>
        /// Subtracts an amount from a user's savings account.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="debtAccountId">The debt account unique identifier.</param>
        /// <param name="request">The details of the amount to subtract.</param>
        Task SubtractAmountFromDebtAccountForUser(Guid userId, Guid debtAccountId, SubtractAmountFromDebtAccountRequest request);

        /// <summary>
        /// Deletes a savings account for a user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="debtAccountId">The debt account unique identifier.</param>
        Task DeleteDebtAccountForUser(Guid userId, Guid debtAccountId);

        /// <summary>
        /// Updates a user's savings account.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="debtAccountId">The debt account unique identifier.</param>
        /// <param name="request">The details of the update.</param>
        Task UpdateDebtAccountForUser(Guid userId, Guid debtAccountId, UpdateDebtAccountRequest request);
    }
}