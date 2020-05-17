using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Controllers.Budgets;
using FinancialPeace.Web.Api.Managers;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.Budgets;
using FinancialPeace.Web.Api.Models.Responses.Budgets;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;

namespace FinancialPeace.Web.Api.Tests.Controllers.Budgets
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class BudgetsControllerTests
    {
        private struct Stubs
        {
            public IBudgetsManager BudgetsManager { get; set; }
        }

        private static Stubs GetStubs()
        {
            var stubs = new Stubs
            {
                BudgetsManager = Substitute.For<IBudgetsManager>()
            };

            return stubs;
        }

        private static BudgetsController GetSystemUnderTest(Stubs stubs)
        {
            return new BudgetsController(stubs.BudgetsManager);
        }

        [Test]
        public void BudgetsController_GivenAllParams_ShouldCreateNewInstance()
        {
            // Arrange
            var stubs = GetStubs();

            // Act
            var controller = new BudgetsController(stubs.BudgetsManager);

            // Assert
            Assert.IsNotNull(controller);
        }

        [Test]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void BudgetsController_GivenNullBudgetsManager_ShouldThrowArgumentNullException()
        {
            // Arrange
            const string expectedParamName = "budgetsManager";

            // Act
            var actual = Assert.Throws<ArgumentNullException>(() => new BudgetsController(null));

            // Assert
            Assert.AreEqual(expectedParamName, actual.ParamName);
        }

        [Test]
        public async Task GetBudgetForUser_GivenUserId_ShouldReturnExpectedOkObjectResult()
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
            stubs.BudgetsManager.GetBudgetForUserAsync(Arg.Any<Guid>()).Returns(expectedResponse);
            var controller = GetSystemUnderTest(stubs);

            // Act
            var result = await controller.GetBudgetForUser(userId).ConfigureAwait(false);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var response = result as ObjectResult;
            var responseBody = response?.Value as GetBudgetForUserResponse;

            responseBody.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public async Task CreateExpenseForUser_GivenUserIdAndRequest_ShouldReturnAcceptedResult()
        {
            // Arrange
            var stubs = GetStubs();
            var controller = GetSystemUnderTest(stubs);

            var request = new CreateExpenseRequest
            {
                ExpenseCategoryName = "Groceries",
                CountryCurrencyCode = "ZAR",
                Value = 3500.0
            };

            // Act
            var result = await controller.CreateExpenseForUser(Guid.NewGuid(), request);

            // Assert
            Assert.IsInstanceOf<AcceptedResult>(result);
        }

        [Test]
        public async Task DeleteExpenseForUser_GivenUserIdAndExpenseId_ShouldReturnOkResult()
        {
            // Arrange
            var stubs = GetStubs();
            var controller = GetSystemUnderTest(stubs);

            // Act
            var result = await controller.DeleteExpenseForUser(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task UpdateExpenseForUser_GivenUserIdAndExpenseId_ShouldReturnOkResult()
        {
            // Arrange
            var stubs = GetStubs();
            var controller = GetSystemUnderTest(stubs);

            var request = new UpdateExpenseRequest
            {
                ExpenseCategoryName = "Rent",
                CountryCurrencyCode = "ZAR",
                Value = 7500.0
            };

            // Act
            var result = await controller.UpdateExpenseForUser(
                Guid.NewGuid(),
                Guid.NewGuid(),
                request);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}