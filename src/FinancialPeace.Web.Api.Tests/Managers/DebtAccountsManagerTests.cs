using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Managers;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.DebtAccounts;
using FinancialPeace.Web.Api.Models.Responses.DebtAccounts;
using FinancialPeace.Web.Api.Repositories;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace FinancialPeace.Web.Api.Tests.Managers
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class DebtAccountsManagerTests
    {
        private struct Stubs
        {
            public IDebtAccountsRepository DebtAccountsRepository { get; set; }
        }

        private static Stubs GetStubs()
        {
            var stubs = new Stubs
            {
                DebtAccountsRepository = Substitute.For<IDebtAccountsRepository>()
            };

            return stubs;
        }

        private static DebtAccountsManager GetSystemUnderTest(Stubs stubs)
        {
            return new DebtAccountsManager(stubs.DebtAccountsRepository);
        }

        [Test]
        public void DebtAccountsManager_GivenAllParams_ShouldCreateNewInstance()
        {
            // Arrange
            var stubs = GetStubs();

            // Act
            var repository = new DebtAccountsManager(stubs.DebtAccountsRepository);

            // Assert
            Assert.IsNotNull(repository);
        }

        [Test]
        public async Task GetDebtAccountForUser_GivenUserId_ShouldReturnExpectedResponse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var debtAccounts = new List<DebtAccount>
            {
                new DebtAccount
                {
                    Name = "Car loan",
                    ActualPayoffDate = null,
                    CountryCurrencyCode = "ZAR",
                    CurrentAmountOwed = 250000,
                    DebtAccountId = Guid.NewGuid(),
                    InitialAmountOwed = 333000,
                    TargetPayoffDate = new DateTime(2020, 6, 30)
                }
            };
            var expectedResponse = new GetDebtAccountsForUserResponse()
            {
                DebtAccounts = debtAccounts,
                UserId = userId
            };

            var stubs = GetStubs();
            stubs.DebtAccountsRepository.GetDebtAccountsForUser(
                    Arg.Any<Guid>())
                .Returns(debtAccounts);
            var manager = GetSystemUnderTest(stubs);

            // Act
            var actualResponse = await manager.GetDebtAccountForUser(userId);

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public void AddDebtAccountForUser_GivenUserIdAndAccountDetails_ShouldCompleteWithoutException()
        {
            // Arrange
            var stubs = GetStubs();
            var manager = GetSystemUnderTest(stubs);

            var request = new AddDebtAccountRequest
            {
                Name = "Home loan",
                AmountOwed = 1095000,
                CountryCurrencyCode = "ZAR",
                TargetPayoffDate = DateTime.Now.AddYears(20)
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await manager.AddDebtAccountForUser(Guid.NewGuid(), request));
        }

        [Test]
        public void AddAmountToDebtAccountForUser_GivenUserIdAndDebtAccountIdAndAmount_ShouldCompleteWithoutException()
        {
            // Arrange
            var stubs = GetStubs();
            var manager = GetSystemUnderTest(stubs);

            var request = new AddAmountToDebtAccountRequest
            {
                Amount = 2500
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await manager.AddAmountToDebtAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid(),
                request));
        }

        [Test]
        public void SubtractAmountFromDebtAccountForUser_GivenUserIdAndDebtAccountIdAndAmount_ShouldCompleteWithoutException()
        {
            // Arrange
            var stubs = GetStubs();
            var manager = GetSystemUnderTest(stubs);

            var request = new SubtractAmountFromDebtAccountRequest
            {
                Amount = 2500
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await manager.SubtractAmountFromDebtAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid(),
                request));
        }

        [Test]
        public void DeleteDebtAccountForUser_GivenUserIdAndDebtAccountId_ShouldCompleteWithoutException()
        {
            // Arrange
            var stubs = GetStubs();
            var manager = GetSystemUnderTest(stubs);

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await manager.DeleteDebtAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid()));
        }

        [Test]
        public void UpdateDebtAccountForUser_GivenUserIdAndDebtAccountIdAndUpdateRequest_ShouldCompleteWithoutException()
        {
            // Arrange
            var stubs = GetStubs();
            var manager = GetSystemUnderTest(stubs);

            var request = new UpdateDebtAccountRequest()
            {
                Name = "New loan",
                ActualPayoffDate = DateTime.Now,
                CountryCurrencyCode = "ZAR",
                CurrentAmountOwed = 0,
                TargetPayoffDate = DateTime.Now
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await manager.UpdateDebtAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid(),
                request));
        }
    }
}