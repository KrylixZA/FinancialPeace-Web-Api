using System;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Models.Requests.Budgets;
using FinancialPeace.Web.Api.Models.Responses.Budgets;
using FinancialPeace.Web.Api.Repositories;
using Microsoft.Extensions.Logging;

namespace FinancialPeace.Web.Api.Managers
{
    /// <inheritdoc />
    public class BudgetsManager : IBudgetsManager
    {
        private readonly IBudgetsRepository _budgetsRepository;
        private readonly ILogger<BudgetsManager> _logger;

        /// <summary>
        /// Initializes a new instance of the Budgets Manager class.
        /// </summary>
        /// <param name="budgetsRepository">The budgets repository.</param>
        /// <param name="logger">The logger.</param>
        public BudgetsManager(
            IBudgetsRepository budgetsRepository,
            ILogger<BudgetsManager> logger)
        {
            _budgetsRepository = budgetsRepository;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<GetBudgetForUserResponse> GetBudgetForUserAsync(Guid userId)
        {
            _logger.LogInformation($"GetBudgetForUserAsync start. UserId: {userId}");
            var userExpenses = await _budgetsRepository.GetBudgetForUserAsync(userId);
            _logger.LogInformation($"GetBudgetForUserAsync end. UserId: {userId}");
            return new GetBudgetForUserResponse
            {
                Expenses = userExpenses,
                UserId = userId
            };
        }

        /// <inheritdoc />
        public Task CreateExpenseForUserAsync(Guid userId, CreateExpenseRequest request)
        {
            _logger.LogInformation($"CreateExpenseForUserAsync start. UserId: {userId}");
            var responseTask = _budgetsRepository.CreateExpenseForUserAsync(userId, request);
            _logger.LogInformation($"CreateExpenseForUserAsync end. UserId: {userId}");
            return responseTask;
        }

        /// <inheritdoc />
        public Task DeleteExpenseForUserAsync(Guid userId, Guid expenseId)
        {
            _logger.LogInformation($"DeleteExpenseForUserAsync start. UserId: {userId}. ExpenseId: {expenseId}");
            var responseTask = _budgetsRepository.DeleteExpenseForUserAsync(userId, expenseId);
            _logger.LogInformation($"DeleteExpenseForUserAsync start. UserId: {userId}. ExpenseId: {expenseId}");
            return responseTask;
        }

        /// <inheritdoc />
        public Task UpdateExpenseForUserAsync(Guid userId, Guid expenseId, UpdateExpenseRequest request)
        {
            _logger.LogInformation($"UpdateExpenseForUserAsync start. UserId: {userId}. ExpenseId: {expenseId}");
            var responseTask = _budgetsRepository.UpdateExpenseForUserAsync(userId, expenseId, request);
            _logger.LogInformation($"UpdateExpenseForUserAsync start. UserId: {userId}. ExpenseId: {expenseId}");
            return responseTask;
        }
    }
}