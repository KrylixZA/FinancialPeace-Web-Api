using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.SavingsAccounts;
using FinancialPeace.Web.Api.Repositories;
using FinancialPeace.Web.Api.Repositories.Connection;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace FinancialPeace.Web.Api.Tests.Repositories
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class SavingsAccountRepositoryTests
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

        private static SavingsAccountRepository GetSystemUnderTest(Stubs stubs)
        {
            return new SavingsAccountRepository(stubs.SqlConnectionProvider);
        }

        [Test]
        public void SavingsAccountRepository_GivenAllParams_ShouldCreateNewInstance()
        {
            // Arrange
            var stubs = GetStubs();

            // Act
            var repository = new SavingsAccountRepository(stubs.SqlConnectionProvider);

            // Assert
            Assert.IsNotNull(repository);
        }

        [Test]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        public void SavingsAccountRepository_GivenNullSqlConnectionProvider_ShouldThrowArgumentNullException()
        {
            // Arrange
            const string expectedParamName = "sqlConnectionProvider";

            // Act
            var actual = Assert.Throws<ArgumentNullException>(() => new SavingsAccountRepository(null));

            // Assert
            Assert.AreEqual(expectedParamName, actual.ParamName);
        }

        [Test]
        public async Task GetSavingsAccountForUser_GivenUserId_ShouldReturnExpectedList()
        {
            // Arrange
            var expectedAccounts = new List<SavingsAccount>
            {
                new SavingsAccount
                {
                    Name = "Emergency savings",
                    SavingsTarget = 150000,
                    CountryCurrencyCode = "ZAR",
                    CurrentSavingsValue = 50107,
                    InitialSavingsValue = 1050,
                    SavingsAccountId = Guid.NewGuid()
                },
                new SavingsAccount
                {
                    Name = "Notice Savings",
                    SavingsTarget = 100000,
                    CountryCurrencyCode = "ZAR",
                    CurrentSavingsValue = 45,
                    InitialSavingsValue = 0,
                    SavingsAccountId = Guid.NewGuid()
                }
            };

            var stubs = GetStubs();
            stubs.SqlConnectionWrapper.QueryAsync<SavingsAccount>(
                    Arg.Any<string>(),
                    Arg.Any<DynamicParameters>(),
                    commandType: Arg.Any<CommandType>())
                .Returns(expectedAccounts);
            var repository = GetSystemUnderTest(stubs);

            // Act
            var actualAccounts = await repository.GetSavingsAccountForUser(Guid.NewGuid());

            // Assert
            actualAccounts.Should().BeEquivalentTo(expectedAccounts);
        }

        [Test]
        public void AddSavingsAccountForUser_GivenUserIdAndAccountDetails_ShouldCompleteWithoutException()
        {
            // Arrange
            var stubs = GetStubs();
            var repository = GetSystemUnderTest(stubs);

            var request = new AddSavingsAccountRequest
            {
                Name = "Emergency savings",
                SavingsTarget = 150000,
                SavingsValue = 50107,
                CountryCurrencyCode = "ZAR"
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await repository.AddSavingsAccountForUser(Guid.NewGuid(), request));
        }

        [Test]
        public void AddAmountToSavingsAccountForUser_GivenUserIdAndSavingsAccountIdAndAmount_ShouldCompleteWithoutException()
        {
            // Arrange
            var stubs = GetStubs();
            var repository = GetSystemUnderTest(stubs);

            var request = new AddAmountToSavingsAccountRequest
            {
                Amount = 2500
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await repository.AddAmountToSavingsAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid(),
                request));
        }

        [Test]
        public void SubtractAmountFromSavingsAccountForUser_GivenUserIdAndSavingsAccountIdAndAmount_ShouldCompleteWithoutException()
        {
            // Arrange
            var stubs = GetStubs();
            var repository = GetSystemUnderTest(stubs);

            var request = new SubtractAmountFromSavingsAccountRequest
            {
                Amount = 2500
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await repository.SubtractAmountFromSavingsAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid(),
                request));
        }

        [Test]
        public void DeleteSavingsAccountForUser_GivenUserIdAndSavingsAccountId_ShouldCompleteWithoutException()
        {
            // Arrange
            var stubs = GetStubs();
            var repository = GetSystemUnderTest(stubs);

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await repository.DeleteSavingsAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid()));
        }

        [Test]
        public void UpdateSavingsAccountForUser_GivenUserIdAndSavingsAccountIdAndUpdateRequest_ShouldCompleteWithoutException()
        {
            // Arrange
            var stubs = GetStubs();
            var repository = GetSystemUnderTest(stubs);

            var request = new UpdateSavingsAccountRequest
            {
                Name = "Notice savings account",
                CountryCurrencyCode = "ZAR",
                CurrentSavingsAmount = 50107,
                TargetSavingsAmount = 100000
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await repository.UpdateSavingsAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid(),
                request));
        }
    }
}