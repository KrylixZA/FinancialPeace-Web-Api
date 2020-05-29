using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.DebtAccounts;
using FinancialPeace.Web.Api.Repositories;
using FinancialPeace.Web.Api.Repositories.Connection;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace FinancialPeace.Web.Api.Tests.Repositories
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class DebtAccountsRepositoryTests
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

        private static DebtAccountsRepository GetSystemUnderTest(Stubs stubs)
        {
            return new DebtAccountsRepository(stubs.SqlConnectionProvider);
        }

        [Test]
        public void DebtAccountsRepository_GivenAllParams_ShouldCreateNewInstance()
        {
            // Arrange
            var stubs = GetStubs();

            // Act
            var repository = new DebtAccountsRepository(stubs.SqlConnectionProvider);

            // Assert
            Assert.IsNotNull(repository);
        }

        [Test]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        public void DebtAccountsRepository_GivenNullSqlConnectionProvider_ShouldThrowArgumentNullException()
        {
            // Arrange
            const string expectedParamName = "sqlConnectionProvider";

            // Act
            var actual = Assert.Throws<ArgumentNullException>(() => new DebtAccountsRepository(null!));

            // Assert
            Assert.AreEqual(expectedParamName, actual.ParamName);
        }

        [Test]
        public async Task GetDebtAccountForUser_GivenUserId_ShouldReturnExpectedList()
        {
            // Arrange
            var expectedAccounts = new List<DebtAccount>
            {
                new DebtAccount
                {
                    Name = "Car loan",
                    ActualPayoffDate = null,
                    CountryCurrencyCode = "ZAR",
                    CurrentAmountOwed = 250000,
                    DebtAccountId = Guid.NewGuid(),
                    InitialAmountOwed = 333000,
                    TargetPayoffDate = new DateTime(2022, 6, 30)
                }
            };

            var stubs = GetStubs();
            stubs.SqlConnectionWrapper.QueryAsync<DebtAccount>(
                    Arg.Any<string>(),
                    Arg.Any<DynamicParameters>(),
                    commandType: Arg.Any<CommandType>())
                .Returns(expectedAccounts);
            var repository = GetSystemUnderTest(stubs);

            // Act
            var actualAccounts = await repository.GetDebtAccountsForUser(Guid.NewGuid());

            // Assert
            actualAccounts.Should().BeEquivalentTo(expectedAccounts);
        }

        [Test]
        public void AddDebtAccountForUser_GivenUserIdAndAccountDetails_ShouldCompleteWithoutException()
        {
            // Arrange
            var stubs = GetStubs();
            var repository = GetSystemUnderTest(stubs);

            var request = new AddDebtAccountRequest
            {
                Name = "House loan",
                AmountOwed = 1095000,
                CountryCurrencyCode = "ZAR",
                TargetPayoffDate = DateTime.Now.AddYears(20)
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await repository.AddDebtAccountForUser(Guid.NewGuid(), request));
        }

        [Test]
        public void AddAmountToDebtAccountForUser_GivenUserIdAndDebtAccountIdAndAmount_ShouldCompleteWithoutException()
        {
            // Arrange
            var stubs = GetStubs();
            var repository = GetSystemUnderTest(stubs);

            var request = new AddAmountToDebtAccountRequest
            {
                Amount = 123456789
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await repository.AddAmountToDebtAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid(),
                request));
        }

        [Test]
        public void SubtractAmountFromDebtAccountForUser_GivenUserIdAndDebtAccountIdAndAmount_ShouldCompleteWithoutException()
        {
            // Arrange
            var stubs = GetStubs();
            var repository = GetSystemUnderTest(stubs);

            var request = new SubtractAmountFromDebtAccountRequest
            {
                Amount = 2500
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await repository.SubtractAmountFromDebtAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid(),
                request));
        }

        [Test]
        public void DeleteDebtAccountForUser_GivenUserIdAndDebtAccountId_ShouldCompleteWithoutException()
        {
            // Arrange
            var stubs = GetStubs();
            var repository = GetSystemUnderTest(stubs);

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await repository.DeleteDebtAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid()));
        }

        [Test]
        public void UpdateDebtAccountForUser_GivenUserIdAndDebtAccountIdAndUpdateRequest_ShouldCompleteWithoutException()
        {
            // Arrange
            var stubs = GetStubs();
            var repository = GetSystemUnderTest(stubs);

            var request = new UpdateDebtAccountRequest
            {
                Name = "New loan",
                CountryCurrencyCode = "ZAR",
                ActualPayoffDate = DateTime.Now,
                CurrentAmountOwed = 0,
                TargetPayoffDate = DateTime.Now
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await repository.UpdateDebtAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid(),
                request));
        }
    }
}