using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Controllers.ExpenseCategories;
using FinancialPeace.Web.Api.Managers;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.ExpenseCategories;
using FinancialPeace.Web.Api.Models.Responses.ExpenseCategories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;

namespace FinancialPeace.Web.Api.Tests.Controllers.ExpenseCategories
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class ExpenseCategoriesControllerTests
    {
        private struct Stubs
        {
            public IExpenseCategoriesManager ExpenseCategoriesManager { get; set; }
        }

        private static Stubs GetStubs()
        {
            var stubs = new Stubs
            {
                ExpenseCategoriesManager = Substitute.For<IExpenseCategoriesManager>()
            };

            return stubs;
        }

        private static ExpenseCategoriesController GetSystemUnderTest(Stubs stubs)
        {
            return new ExpenseCategoriesController(stubs.ExpenseCategoriesManager);
        }

        [Test]
        public void ExpenseCategoriesController_GivenAllParams_ShouldCreateNewInstance()
        {
            // Arrange
            var stubs = GetStubs();

            // Act
            var controller = new ExpenseCategoriesController(stubs.ExpenseCategoriesManager);

            // Assert
            Assert.IsNotNull(controller);
        }

        [Test]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        public void ExpenseCategoriesController_GivenNullExpenseCategoriesManager_ShouldThrowArgumentNullException()
        {
            // Arrange
            const string expectedParamName = "expenseCategoriesManager";

            // Act
            var actual = Assert.Throws<ArgumentNullException>(() => new ExpenseCategoriesController(null));

            // Assert
            Assert.AreEqual(expectedParamName, actual.ParamName);
        }

        [Test]
        public async Task GetExpenseCategories_OnRequest_ShouldReturnExpectedOkObjectResult()
        {
            // Arrange
            var expenseCategories = new List<ExpenseCategory>
            {
                new ExpenseCategory
                {
                    ExpenseCategoryId = Guid.NewGuid(),
                    ExpenseCategoryName = "Rent"
                },
                new ExpenseCategory
                {
                    ExpenseCategoryId = Guid.NewGuid(),
                    ExpenseCategoryName = "Groceries"
                },
                new ExpenseCategory
                {
                    ExpenseCategoryId = Guid.NewGuid(),
                    ExpenseCategoryName = "Utilities"
                }
            };
            var expectedResponse = new GetExpenseCategoriesResponse
            {
                ExpenseCategories = expenseCategories
            };

            var stubs = GetStubs();
            stubs.ExpenseCategoriesManager.GetExpenseCategories().Returns(expectedResponse);
            var controller = GetSystemUnderTest(stubs);

            // Act
            var result = await controller.GetExpenseCategories();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var response = result as ObjectResult;
            var responseBody = response?.Value as GetExpenseCategoriesResponse;

            responseBody.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public async Task GetExpenseCategoriesForUser_GivenUserId_ShouldReturnExpectedOkObjectResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expenseCategories = new List<ExpenseCategory>
            {
                new ExpenseCategory
                {
                    ExpenseCategoryId = Guid.NewGuid(),
                    ExpenseCategoryName = "Rent"
                },
                new ExpenseCategory
                {
                    ExpenseCategoryId = Guid.NewGuid(),
                    ExpenseCategoryName = "Groceries"
                },
                new ExpenseCategory
                {
                    ExpenseCategoryId = Guid.NewGuid(),
                    ExpenseCategoryName = "Utilities"
                }
            };
            var expectedResponse = new GetExpenseCategoriesForUserResponse()
            {
                UserId = userId,
                ExpenseCategories = expenseCategories
            };

            var stubs = GetStubs();
            stubs.ExpenseCategoriesManager.GetExpenseCategoriesForUser(Arg.Any<Guid>()).Returns(expectedResponse);
            var controller = GetSystemUnderTest(stubs);

            // Act
            var result = await controller.GetExpenseCategoriesForUser(userId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var response = result as ObjectResult;
            var responseBody = response?.Value as GetExpenseCategoriesForUserResponse;

            responseBody.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public async Task AddExpenseCategoryForUser_GivenUserIdAndRequest_ShouldReturnAcceptedResult()
        {
            // Arrange
            var stubs = GetStubs();
            var controller = GetSystemUnderTest(stubs);

            var request = new AddExpenseCategoryRequest
            {
                ExpenseCategoryName = "Test"
            };

            // Act
            var result = await controller.AddExpenseCategoryForUser(Guid.NewGuid(), request);

            // Assert
            Assert.IsInstanceOf<AcceptedResult>(result);
        }
        
        [Test]
        public async Task DeleteExpenseCategoryForUser_GivenUserIdAndExpenseCategoryId_ShouldReturnOkResult()
        {
            // Arrange
            var stubs = GetStubs();
            var controller = GetSystemUnderTest(stubs);

            // Act
            var result = await controller.DeleteExpenseCategoryForUser(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}