using System;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Models.Requests.ExpenseCategories;
using FinancialPeace.Web.Api.Models.Responses.ExpenseCategories;
using FinancialPeace.Web.Api.Repositories;

namespace FinancialPeace.Web.Api.Managers
{
    /// <inheritdoc />
    public class ExpenseCategoriesManager : IExpenseCategoriesManager
    {
        private readonly IExpenseCategoriesRepository _expenseCategoriesRepository;

        /// <summary>
        /// Creates a new instance of the Expense Categories Manager class.
        /// </summary>
        /// <param name="expenseCategoriesRepository">The expense categories repository.</param>
        public ExpenseCategoriesManager(IExpenseCategoriesRepository expenseCategoriesRepository)
        {
            _expenseCategoriesRepository = expenseCategoriesRepository;
        }

        /// <inheritdoc />
        public async Task<GetExpenseCategoriesResponse> GetExpenseCategories()
        {
            var expenseCategories = await _expenseCategoriesRepository.GetExpenseCategories().ConfigureAwait(false);
            return new GetExpenseCategoriesResponse
            {
                ExpenseCategories = expenseCategories
            };
        }

        /// <inheritdoc />
        public async Task<GetExpenseCategoriesForUserResponse> GetExpenseCategoriesForUser(Guid userId)
        {
            var expenseCategories = await _expenseCategoriesRepository.GetExpenseCategoriesForUser(userId).ConfigureAwait(false);
            return new GetExpenseCategoriesForUserResponse
            {
                ExpenseCategories = expenseCategories,
                UserId = userId
            };
        }

        /// <inheritdoc />
        public Task AddExpenseCategoryForUser(Guid userId, AddExpenseCategoryRequest request)
        {
            return _expenseCategoriesRepository.AddExpenseCategoryForUser(userId, request);
        }

        /// <inheritdoc />
        public Task DeleteExpenseCategoryForUser(Guid userId, Guid expenseCategoryId)
        {
            return _expenseCategoriesRepository.DeleteExpenseCategoryForUser(userId, expenseCategoryId);
        }
    }
}