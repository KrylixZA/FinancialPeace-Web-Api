using System;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Models.Requests.Budgets;
using FinancialPeace.Web.Api.Models.Responses.Budgets;
using FinancialPeace.Web.Api.Repositories;

namespace FinancialPeace.Web.Api.Managers
{
    /// <inheritdoc />
    public class BudgetsManager : IBudgetsManager
    {
        private readonly IBudgetsRepository _budgetsRepository;

        /// <summary>
        /// Initializes a new instance of the Budgets Manager class.
        /// </summary>
        /// <param name="budgetsRepository">The budgets repository.</param>
        public BudgetsManager(IBudgetsRepository budgetsRepository)
        {
            _budgetsRepository = budgetsRepository;
        }

        /// <inheritdoc />
        public async Task<GetBudgetForUserResponse> GetBudgetForUserAsync(Guid userId)
        {
            var userExpenses = await _budgetsRepository.GetBudgetForUserAsync(userId).ConfigureAwait(false);
            return new GetBudgetForUserResponse
            {
                Expenses = userExpenses,
                UserId = userId
            };
        }

        /// <inheritdoc />
        public Task CreateExpenseForUserAsync(Guid userId, CreateExpenseRequest request)
        {
            return _budgetsRepository.CreateExpenseForUserAsync(userId, request);
        }

        /// <inheritdoc />
        public Task DeleteExpenseForUserAsync(Guid userId, Guid expenseId)
        {
            return _budgetsRepository.DeleteExpenseForUserAsync(userId, expenseId);
        }

        /// <inheritdoc />
        public Task UpdateExpenseForUserAsync(Guid userId, Guid expenseId, UpdateExpenseRequest request)
        {
            return _budgetsRepository.UpdateExpenseForUserAsync(userId, expenseId, request);
        }
    }
}