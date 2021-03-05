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
using Microsoft.Extensions.Logging;
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
            public static ILogger<DebtAccountsRepository> Logger => Substitute.For<ILogger<DebtAccountsRepository>>();
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
            return new DebtAccountsRepository(stubs.SqlConnectionProvider, Stubs.Logger);
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
            var actualAccounts = await repository.GetDebtAccountsForUserAsync(Guid.NewGuid());

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
            Assert.DoesNotThrowAsync(async () => await repository.AddDebtAccountForUserAsync(Guid.NewGuid(), request));
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
            Assert.DoesNotThrowAsync(async () => await repository.AddAmountToDebtAccountForUserAsync(
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
            Assert.DoesNotThrowAsync(async () => await repository.SubtractAmountFromDebtAccountForUserAsync(
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
            Assert.DoesNotThrowAsync(async () => await repository.DeleteDebtAccountForUserAsync(
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
            Assert.DoesNotThrowAsync(async () => await repository.UpdateDebtAccountForUserAsync(
                Guid.NewGuid(),
                Guid.NewGuid(),
                request));
        }
    }
}