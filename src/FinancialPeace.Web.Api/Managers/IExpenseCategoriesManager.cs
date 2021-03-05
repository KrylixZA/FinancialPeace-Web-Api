using System;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Models.Requests.ExpenseCategories;
using FinancialPeace.Web.Api.Models.Responses.ExpenseCategories;

namespace FinancialPeace.Web.Api.Managers
{
    /// <summary>
    /// An interface for a manager layer that intercedes between the expense categories repository and the controller.
    /// </summary>
    public interface IExpenseCategoriesManager
    {
        /// <summary>
        /// Gets all listed expense categories.
        /// </summary>
        /// <returns>An enumeration of expense categories.</returns>
        Task<GetExpenseCategoriesResponse> GetExpenseCategoriesAsync();

        /// <summary>
        /// Gets all listed expense categories linked to a user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <returns>An enumeration of expense categories linked to a user.</returns>
        Task<GetExpenseCategoriesForUserResponse> GetExpenseCategoriesForUserAsync(Guid userId);

        /// <summary>
        /// Creates a link between a user and an expense category. If the expense category does not exist, it will be created first. The created expense category will be available for all users.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="request">The expense category details.</param>
        Task AddExpenseCategoryForUserAsync(Guid userId, AddExpenseCategoryRequest request);

        /// <summary>
        /// Deletes a mapping between a user and an expense category.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="expenseCategoryId">The expense category's unique identifier.</param>
        Task DeleteExpenseCategoryForUserAsync(Guid userId, Guid expenseCategoryId);
    }
}