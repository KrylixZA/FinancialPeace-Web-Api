using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Managers;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.ExpenseCategories;
using FinancialPeace.Web.Api.Models.Responses.ExpenseCategories;
using FinancialPeace.Web.Api.Repositories;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace FinancialPeace.Web.Api.Tests.Managers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class ExpenseCategoriesManagerTests
    {
        private struct Stubs
        {
            public IExpenseCategoriesRepository ExpenseCategoriesRepository { get; set; }
        }

        private static Stubs GetStubs()
        {
            var stubs = new Stubs
            {
                ExpenseCategoriesRepository = Substitute.For<IExpenseCategoriesRepository>()
            };

            return stubs;
        }

        private static ExpenseCategoriesManager GetSystemUnderTest(Stubs stubs)
        {
            return new ExpenseCategoriesManager(stubs.ExpenseCategoriesRepository);
        }

        [Test]
        public void ExpenseCategoriesManager_GivenAllParams_ShouldCreateNewInstance()
        {
            // Arrange
            var stubs = GetStubs();

            // Act
            var manager = new ExpenseCategoriesManager(stubs.ExpenseCategoriesRepository);

            // Assert
            Assert.IsNotNull(manager);
        }

        [Test]
        public async Task GetExpenseCategories_OnRequest_ShouldReturnExpectedExpenseCategoriesResponse()
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
            stubs.ExpenseCategoriesRepository.GetExpenseCategories().Returns(expenseCategories);
            var manager = GetSystemUnderTest(stubs);

            // Act
            var actualResponse = await manager.GetExpenseCategories();

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }
        
        [Test]
        public async Task GetExpenseCategoriesForUser_GivenUserId_ShouldReturnExpectedExpenseCategoriesResponse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expenseCategoriesForUser = new List<ExpenseCategory>
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
                ExpenseCategories = expenseCategoriesForUser
            };

            var stubs = GetStubs();
            stubs.ExpenseCategoriesRepository.GetExpenseCategoriesForUser(Arg.Any<Guid>()).Returns(expenseCategoriesForUser);
            var manager = GetSystemUnderTest(stubs);

            // Act
            var actualResponse = await manager.GetExpenseCategoriesForUser(userId);

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public void AddExpenseCategoryForUser_GivenUserIdAndRequest_ShouldCompleteWithoutError()
        {
            // Arrange
            var stubs = GetStubs();
            var manager = GetSystemUnderTest(stubs);

            var request = new AddExpenseCategoryRequest
            {
                ExpenseCategoryName = "Test"
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await manager.AddExpenseCategoryForUser(Guid.NewGuid(), request));
        }
        
        [Test]
        public void DeleteExpenseCategoryForUser_GivenUserIdAndRequest_ShouldCompleteWithoutError()
        {
            // Arrange
            var stubs = GetStubs();
            var manager = GetSystemUnderTest(stubs);

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await manager.DeleteExpenseCategoryForUser(Guid.NewGuid(), Guid.NewGuid()));
        }
    }
}