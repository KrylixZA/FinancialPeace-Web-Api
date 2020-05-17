using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.ExpenseCategories;

namespace FinancialPeace.Web.Api.Repositories
{
    /// <summary>
    /// Provides a contract for interacting with the expense categories parts of the database.
    /// </summary>
    public interface IExpenseCategoriesRepository
    {
        /// <summary>
        /// Gets all the available expense categories from the database.
        /// </summary>
        /// <returns>An enumeration of expense categories.</returns>
        Task<IEnumerable<ExpenseCategory>> GetExpenseCategories();

        /// <summary>
        /// Gets all the expense categories linked to the user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <returns>An enumeration of expense categories linked to the user.</returns>
        Task<IEnumerable<ExpenseCategory>> GetExpenseCategoriesForUser(Guid userId);

        /// <summary>
        /// Links a user to an expense category. If the expense category does not exist, it will be created first and become available to all users.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="request">The expense category details.</param>
        Task AddExpenseCategoryForUser(Guid userId, AddExpenseCategoryRequest request);

        /// <summary>
        /// Deletes the mapping between an expense category and a user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="expenseCategoryId">The expense category's unique identifier.</param>
        Task DeleteExpenseCategoryForUser(Guid userId, Guid expenseCategoryId);
    }
}