using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Managers;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.SavingsAccounts;
using FinancialPeace.Web.Api.Models.Responses.SavingsAccounts;
using FinancialPeace.Web.Api.Repositories;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace FinancialPeace.Web.Api.Tests.Managers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class SavingsAccountManagerTests
    {
        private struct Stubs
        {
            public ISavingsAccountRepository SavingsAccountRepository { get; set; }
        }

        private static Stubs GetStubs()
        {
            var stubs = new Stubs
            {
                SavingsAccountRepository = Substitute.For<ISavingsAccountRepository>()
            };

            return stubs;
        }

        private static SavingsAccountManager GetSystemUnderTest(Stubs stubs)
        {
            return new SavingsAccountManager(stubs.SavingsAccountRepository);
        }

        [Test]
        public void SavingsAccountManager_GivenAllParams_ShouldCreateNewInstance()
        {
            // Arrange
            var stubs = GetStubs();

            // Act
            var repository = new SavingsAccountManager(stubs.SavingsAccountRepository);

            // Assert
            Assert.IsNotNull(repository);
        }

        [Test]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        public void SavingsAccountManager_GivenNullSavingsAccountRepository_ShouldThrowArgumentNullException()
        {
            // Arrange
            const string expectedParamName = "savingsAccountRepository";

            // Act
            var actual = Assert.Throws<ArgumentNullException>(() => new SavingsAccountManager(null!));

            // Assert
            Assert.AreEqual(expectedParamName, actual.ParamName);
        }

        [Test]
        public async Task GetSavingsAccountForUser_GivenUserId_ShouldReturnExpectedResponse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var savingsAccounts = new List<SavingsAccount>
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
            var expectedResponse = new GetSavingsAccountForUserResponse
            {
                SavingsAccounts = savingsAccounts,
                UserId = userId
            };

            var stubs = GetStubs();
            stubs.SavingsAccountRepository.GetSavingsAccountForUser(
                    Arg.Any<Guid>())
                .Returns(savingsAccounts);
            var manager = GetSystemUnderTest(stubs);

            // Act
            var actualResponse = await manager.GetSavingsAccountForUser(userId);

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public void AddSavingsAccountForUser_GivenUserIdAndAccountDetails_ShouldCompleteWithoutException()
        {
            // Arrange
            var stubs = GetStubs();
            var manager = GetSystemUnderTest(stubs);

            var request = new AddSavingsAccountRequest
            {
                Name = "Emergency savings",
                SavingsTarget = 150000,
                SavingsValue = 50107,
                CountryCurrencyCode = "ZAR"
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await manager.AddSavingsAccountForUser(Guid.NewGuid(), request));
        }

        [Test]
        public void AddAmountToSavingsAccountForUser_GivenUserIdAndSavingsAccountIdAndAmount_ShouldCompleteWithoutException()
        {
            // Arrange
            var stubs = GetStubs();
            var manager = GetSystemUnderTest(stubs);

            var request = new AddAmountToSavingsAccountRequest
            {
                Amount = 2500
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await manager.AddAmountToSavingsAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid(),
                request));
        }

        [Test]
        public void SubtractAmountFromSavingsAccountForUser_GivenUserIdAndSavingsAccountIdAndAmount_ShouldCompleteWithoutException()
        {
            // Arrange
            var stubs = GetStubs();
            var manager = GetSystemUnderTest(stubs);

            var request = new SubtractAmountFromSavingsAccountRequest
            {
                Amount = 2500
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await manager.SubtractAmountFromSavingsAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid(),
                request));
        }

        [Test]
        public void DeleteSavingsAccountForUser_GivenUserIdAndSavingsAccountId_ShouldCompleteWithoutException()
        {
            // Arrange
            var stubs = GetStubs();
            var manager = GetSystemUnderTest(stubs);

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await manager.DeleteSavingsAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid()));
        }

        [Test]
        public void UpdateSavingsAccountForUser_GivenUserIdAndSavingsAccountIdAndUpdateRequest_ShouldCompleteWithoutException()
        {
            // Arrange
            var stubs = GetStubs();
            var manager = GetSystemUnderTest(stubs);

            var request = new UpdateSavingsAccountRequest
            {
                Name = "Notice savings account",
                CountryCurrencyCode = "ZAR",
                CurrentSavingsAmount = 50107,
                TargetSavingsAmount = 100000
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await manager.UpdateSavingsAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid(),
                request));
        }
    }
}