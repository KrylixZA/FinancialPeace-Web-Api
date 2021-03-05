using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Managers;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.Budgets;
using FinancialPeace.Web.Api.Models.Responses.Budgets;
using FinancialPeace.Web.Api.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace FinancialPeace.Web.Api.Tests.Managers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class BudgetsManagerTests
    {
        private struct Stubs
        {
            public IBudgetsRepository BudgetsRepository { get; set; }
            public static ILogger<BudgetsManager> Logger => Substitute.For<ILogger<BudgetsManager>>();
        }

        private static Stubs GetStubs()
        {
            var stubs = new Stubs
            {
                BudgetsRepository = Substitute.For<IBudgetsRepository>()
            };

            return stubs;
        }

        private static BudgetsManager GetSystemUnderTest(Stubs stubs)
        {
            return new BudgetsManager(stubs.BudgetsRepository, Stubs.Logger);
        }

        [Test]
        public async Task GetBudgetForUserAsync_GivenUserId_ShouldReturnExpectedResponse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expenses = new List<Expense>
            {
                new Expense
                {
                    BudgetId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    ExpenseId = Guid.NewGuid(),
                    DisplayName = "Groceries",
                    CountryCurrencyCode = "ZAR",
                    Value = 3500.0
                }
            };
            var expectedResponse = new GetBudgetForUserResponse
            {
                UserId = userId,
                Expenses = expenses
            };
            
            var stubs = GetStubs();
            stubs.BudgetsRepository.GetBudgetForUserAsync(Arg.Any<Guid>()).Returns(expenses);
            var manager = GetSystemUnderTest(stubs);

            // Act
            var actualResponse = await manager.GetBudgetForUserAsync(userId);

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }
        
        [Test]
        public void CreateExpenseForUserAsync_GivenUserIdAndCreateRequest_ShouldCompleteTransactionWithoutError()
        {
            // Arrange
            var stubs = GetStubs();
            var manager = GetSystemUnderTest(stubs);

            var request = new CreateExpenseRequest
            {
                ExpenseCategoryName = "Groceries",
                CountryCurrencyCode = "ZAR",
                Value = 3500.0
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await manager.CreateExpenseForUserAsync(Guid.NewGuid(), request));
        }

        [Test]
        public void DeleteExpenseForUserAsync_GivenUserIdAndExpenseId_ShouldCompleteWithoutError()
        {
            // Arrange
            var stubs = GetStubs();
            var manager = GetSystemUnderTest(stubs);

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await manager.DeleteExpenseForUserAsync(
                Guid.NewGuid(),
                Guid.NewGuid()));
        }

        [Test]
        public void UpdateExpenseForUserAsync_GivenUserIdAndExpenseIdAndUpdateRequest_ShouldCompleteWithoutError()
        {
            // Arrange
            var stubs = GetStubs();
            var manager = GetSystemUnderTest(stubs);

            var request = new UpdateExpenseRequest
            {
                ExpenseCategoryName = "Rent",
                CountryCurrencyCode = "ZAR",
                Value = 7500.0
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await manager.UpdateExpenseForUserAsync(
                Guid.NewGuid(), 
                Guid.NewGuid(),
                request));
        }
    }
}