using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.ExpenseCategories;
using FinancialPeace.Web.Api.Repositories;
using FinancialPeace.Web.Api.Repositories.Connection;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace FinancialPeace.Web.Api.Tests.Repositories
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class ExpenseCategoriesRepositoryTests
    {
        private struct Stubs
        {
            public ISqlConnectionProvider SqlConnectionProvider { get; set; }
            public ISqlConnectionWrapper SqlConnectionWrapper { get; set; }
        }

        private static Stubs GetStubs()
        {
            var stubs = new Stubs
            {
                SqlConnectionProvider = Substitute.For<ISqlConnectionProvider>(),
                SqlConnectionWrapper = Substitute.For<ISqlConnectionWrapper>()
            };

            stubs.SqlConnectionProvider.Open().Returns(stubs.SqlConnectionWrapper);

            return stubs;
        }

        private static ExpenseCategoriesRepository GetSystemUnderTest(Stubs stubs)
        {
            return new ExpenseCategoriesRepository(stubs.SqlConnectionProvider);
        }

        [Test]
        public void ExpenseCategoriesRepository_GivenAllParams_ShouldCreateNewInstance()
        {
            // Arrange
            var stubs = GetStubs();

            // Act
            var repository = GetSystemUnderTest(stubs);

            // Assert
            Assert.IsNotNull(repository);
        }

        [Test]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void ExpenseCategoriesRepository_GivenNullConnectionProvider_ShouldThrowArgumentNullException()
        {
            // Arrange
            const string expectedParamName = "connectionProvider";

            // Act
            var actual = Assert.Throws<ArgumentNullException>(() => new ExpenseCategoriesRepository(null));

            // Assert
            Assert.AreEqual(expectedParamName, actual.ParamName);
        }

        [Test]
        public async Task GetExpenseCategories_OnRequest_ShouldReturnExpectedCategories()
        {
            // Arrange
            var expectedResponse = new List<ExpenseCategory>
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

            var stubs = GetStubs();
            stubs.SqlConnectionWrapper.QueryAsync<ExpenseCategory>(
                    Arg.Any<string>(),
                    commandType: Arg.Any<CommandType>())
                .Returns(expectedResponse);
            var repository = GetSystemUnderTest(stubs);

            // Act
            var actualResponse = await repository.GetExpenseCategories();

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public async Task GetExpenseCategoriesForUser_GivenUserId_ShouldReturnExpectedCategories()
        {
            // Arrange
            var expectedResponse = new List<ExpenseCategory>
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

            var stubs = GetStubs();
            stubs.SqlConnectionWrapper.QueryAsync<ExpenseCategory>(
                    Arg.Any<string>(),
                    Arg.Any<DynamicParameters>(),
                    commandType: Arg.Any<CommandType>())
                .Returns(expectedResponse);
            var repository = GetSystemUnderTest(stubs);

            // Act
            var actualResponse = await repository.GetExpenseCategoriesForUser(Guid.NewGuid());

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public void AddExpenseCategoryForUser_GivenUserIdAndRequest_ShouldCompleteTransactionWithoutError()
        {
            // Arrange
            var stubs = GetStubs();
            var repository = GetSystemUnderTest(stubs);

            var request = new AddExpenseCategoryRequest
            {
                ExpenseCategoryName = "Test"
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async() => await repository.AddExpenseCategoryForUser(
                Guid.NewGuid(), 
                request));
        }
        
        [Test]
        public void DeleteExpenseCategoryForUser_GivenUserIdAndExpenseCategoryId_ShouldCompleteTransactionWithoutError()
        {
            // Arrange
            var stubs = GetStubs();
            var repository = GetSystemUnderTest(stubs);

            // Act & Assert
            Assert.DoesNotThrowAsync(async() => await repository.DeleteExpenseCategoryForUser(
                Guid.NewGuid(), 
                Guid.NewGuid()));
        }
    }
}