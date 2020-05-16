using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.Budgets;

namespace FinancialPeace.Web.Api.Repositories
{
    /// <summary>
    /// Provides a contract for accessing the budgets portion of the Freedom database.
    /// </summary>
    public interface IBudgetsRepository
    {
        /// <summary>
        /// Gets the budget for a user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <returns>An enumeration of expenses for the user.</returns>
        Task<IEnumerable<Expense>> GetBudgetForUserAsync(Guid userId);

        /// <summary>
        /// Attempts to create an expense for a user, creating a budget for the user if none exists.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="request">The details of the expense.</param>
        Task CreateExpenseForUserAsync(Guid userId, CreateExpenseRequest request);

        /// <summary>
        /// Attempts to delete an expense for a user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="expenseId">The expense unique identifier.</param>
        Task DeleteExpenseForUserAsync(Guid userId, Guid expenseId);

        /// <summary>
        /// Attempts to update an expense for a user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="expenseId">The expense unique identifier.</param>
        /// <param name="request">The updated details of the request.</param>
        Task UpdateExpenseForUserAsync(Guid userId, Guid expenseId, UpdateExpenseRequest request);
    }
}