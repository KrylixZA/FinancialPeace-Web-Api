using System;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Models.Requests.Budgets;
using FinancialPeace.Web.Api.Models.Responses.Budgets;

namespace FinancialPeace.Web.Api.Managers
{
    /// <summary>
    /// An interface for a manager layer that will interpret results from the DB layer and transform them to a useful model for the controller.
    /// </summary>
    public interface IBudgetsManager
    {
        /// <summary>
        /// Gets the budget for a user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <returns>An enumeration of expenses for the user.</returns>
        Task<GetBudgetForUserResponse> GetBudgetForUserAsync(Guid userId);

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