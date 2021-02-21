using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Controllers.DebtAccounts;
using FinancialPeace.Web.Api.Managers;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.DebtAccounts;
using FinancialPeace.Web.Api.Models.Responses.DebtAccounts;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;

namespace FinancialPeace.Web.Api.Tests.Controllers.DebtAccounts
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class DebtAccountsControllerTests
    {
        private struct Stubs
        {
            public IDebtAccountsManager DebtAccountsManager { get; set; }
        }

        private static Stubs GetStubs()
        {
            var stubs = new Stubs
            {
                DebtAccountsManager = Substitute.For<IDebtAccountsManager>()
            };

            return stubs;
        }

        private static DebtAccountsController GetSystemUnderTest(Stubs stubs)
        {
            return new DebtAccountsController(stubs.DebtAccountsManager);
        }

        [Test]
        public void DebtAccountsController_GivenAllParams_ShouldCreateNewInstance()
        {
            // Arrange
            var stubs = GetStubs();

            // Act
            var controller = new DebtAccountsController(stubs.DebtAccountsManager);

            // Assert
            Assert.IsNotNull(controller);
        }

        [Test]
        public async Task GetDebtAccountsForUser_GivenUserId_ShouldReturnOkObjectResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expectedResponse = new GetDebtAccountsForUserResponse
            {
                DebtAccounts = new List<DebtAccount>
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
                },
                UserId = userId
            };

            var stubs = GetStubs();
            stubs.DebtAccountsManager.GetDebtAccountForUser(Arg.Any<Guid>()).Returns(expectedResponse);
            var controller = GetSystemUnderTest(stubs);

            // Act
            var result = await controller.GetDebtAccountsForUser(userId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var response = result as ObjectResult;
            var responseBody = response?.Value as GetDebtAccountsForUserResponse;

            responseBody.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public async Task AddDebtAccountForUser_GivenUserIdAndAccountDetails_ShouldReturnAcceptedResult()
        {
            // Arrange
            var stubs = GetStubs();
            var controller = GetSystemUnderTest(stubs);

            var request = new AddDebtAccountRequest
            {
                Name = "Home loan",
                AmountOwed = 1095000,
                CountryCurrencyCode = "ZAR",
                TargetPayoffDate = DateTime.Now.AddYears(20)
            };

            // Act
            var result = await controller.AddDebtAccountForUser(Guid.NewGuid(), request);

            // Assert
            Assert.IsInstanceOf<AcceptedResult>(result);
        }

        [Test]
        public async Task AddAmountToDebtAccountForUser_GivenUserIdAndDebtAccountIdAndAmount_ShouldOkResult()
        {
            // Arrange
            var stubs = GetStubs();
            var controller = GetSystemUnderTest(stubs);

            var request = new AddAmountToDebtAccountRequest()
            {
                Amount = 2500
            };

            // Act
            var result = await controller.AddAmountToDebtAccount(
                Guid.NewGuid(),
                Guid.NewGuid(),
                request);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task SubtractAmountFromDebtAccountForUser_GivenUserIdAndDebtAccountIdAndAmount_ShouldReturnOkResult()
        {
            // Arrange
            var stubs = GetStubs();
            var controller = GetSystemUnderTest(stubs);

            var request = new SubtractAmountFromDebtAccountRequest()
            {
                Amount = 2500
            };

            // Act
            var result = await controller.SubtractAmountFromDebtAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid(),
                request);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task DeleteDebtAccountForUser_GivenUserIdAndDebtAccountId_ShouldOkResult()
        {
            // Arrange
            var stubs = GetStubs();
            var controller = GetSystemUnderTest(stubs);

            // Act
            var result = await controller.DeleteDebtAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid());
            
            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task UpdateDebtAccountForUser_GivenUserIdAndDebtAccountIdAndUpdateRequest_ShouldReturnOkResult()
        {
            // Arrange
            var stubs = GetStubs();
            var controller = GetSystemUnderTest(stubs);

            var request = new UpdateDebtAccountRequest()
            {
                Name = "New loan",
                ActualPayoffDate = DateTime.Now,
                CountryCurrencyCode = "ZAR",
                CurrentAmountOwed = 0,
                TargetPayoffDate = DateTime.Now
            };

            // Act
            var result = await controller.UpdateDebtAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid(),
                request);
            
            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}