using System;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Models.Requests.ExpenseCategories;
using FinancialPeace.Web.Api.Models.Responses.ExpenseCategories;
using FinancialPeace.Web.Api.Repositories;
using Microsoft.Extensions.Logging;

namespace FinancialPeace.Web.Api.Managers
{
    /// <inheritdoc />
    public class ExpenseCategoriesManager : IExpenseCategoriesManager
    {
        private readonly IExpenseCategoriesRepository _expenseCategoriesRepository;
        private readonly ILogger<ExpenseCategoriesManager> _logger;

        /// <summary>
        /// Creates a new instance of the Expense Categories Manager class.
        /// </summary>r
        /// <param name="expenseCategoriesRepository">The expense categories repository.</param>
        /// <param name="logger">The logger.</param>
        public ExpenseCategoriesManager(
            IExpenseCategoriesRepository expenseCategoriesRepository,
            ILogger<ExpenseCategoriesManager> logger)
        {
            _expenseCategoriesRepository = expenseCategoriesRepository;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<GetExpenseCategoriesResponse> GetExpenseCategoriesAsync()
        {
            _logger.LogInformation("GetExpenseCategoriesAsync start");
            var expenseCategories = await _expenseCategoriesRepository.GetExpenseCategoriesAsync();
            _logger.LogInformation("GetExpenseCategoriesAsync end");
            return new GetExpenseCategoriesResponse
            {
                ExpenseCategories = expenseCategories
            };
        }

        /// <inheritdoc />
        public async Task<GetExpenseCategoriesForUserResponse> GetExpenseCategoriesForUserAsync(Guid userId)
        {
            _logger.LogInformation($"GetExpenseCategoriesForUserAsync start. UserId: {userId}");
            var expenseCategories = await _expenseCategoriesRepository.GetExpenseCategoriesForUserAsync(userId);
            _logger.LogInformation($"GetExpenseCategoriesForUserAsync end. UserId: {userId}");
            return new GetExpenseCategoriesForUserResponse
            {
                ExpenseCategories = expenseCategories,
                UserId = userId
            };
        }

        /// <inheritdoc />
        public Task AddExpenseCategoryForUserAsync(Guid userId, AddExpenseCategoryRequest request)
        {
            _logger.LogInformation($"AddExpenseCategoryForUserAsync start. UserId: {userId}");
            var responseTask = _expenseCategoriesRepository.AddExpenseCategoryForUserAsync(userId, request);
            _logger.LogInformation($"AddExpenseCategoryForUserAsync end. UserId: {userId}");
            return responseTask;
        }

        /// <inheritdoc />
        public Task DeleteExpenseCategoryForUserAsync(Guid userId, Guid expenseCategoryId)
        {
            _logger.LogInformation($"DeleteExpenseCategoryForUserAsync start. UserId: {userId}. ExpenseCategoryId: {expenseCategoryId}");
            var responseTask = _expenseCategoriesRepository.DeleteExpenseCategoryForUserAsync(userId, expenseCategoryId);
            _logger.LogInformation($"DeleteExpenseCategoryForUserAsync end. UserId: {userId}. ExpenseCategoryId: {expenseCategoryId}");
            return responseTask;
        }
    }
}