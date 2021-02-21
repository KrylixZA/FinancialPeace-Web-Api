using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Controllers.SavingsAccounts;
using FinancialPeace.Web.Api.Managers;
using FinancialPeace.Web.Api.Models.Requests.SavingsAccounts;
using FinancialPeace.Web.Api.Models.Responses.SavingsAccounts;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;

namespace FinancialPeace.Web.Api.Tests.Controllers.SavingsAccount
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class SavingsAccountControllerTests
    {
        private struct Stubs
        {
            public ISavingsAccountManager SavingsAccountManager { get; set; }
        }

        private static Stubs GetStubs()
        {
            var stubs = new Stubs
            {
                SavingsAccountManager = Substitute.For<ISavingsAccountManager>()
            };

            return stubs;
        }

        private static SavingsAccountController GetSystemUnderTest(Stubs stubs)
        {
            return new SavingsAccountController(stubs.SavingsAccountManager);
        }

        [Test]
        public void SavingsAccountController_GivenAllParams_ShouldCreateNewInstance()
        {
            // Arrange
            var stubs = GetStubs();

            // Act
            var controller = new SavingsAccountController(stubs.SavingsAccountManager);

            // Assert
            Assert.IsNotNull(controller);
        }

        [Test]
        public async Task GetSavingsAccountsForUser_GivenUserId_ShouldReturnOkObjectResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expectedResponse = new GetSavingsAccountForUserResponse
            {
                SavingsAccounts = new List<Models.SavingsAccount>
                {
                    new Models.SavingsAccount
                    {
                        Name = "Emergency savings",
                        SavingsTarget = 150000,
                        CountryCurrencyCode = "ZAR",
                        CurrentSavingsValue = 50107,
                        InitialSavingsValue = 1000,
                        SavingsAccountId = Guid.NewGuid()
                    }
                },
                UserId = userId
            };

            var stubs = GetStubs();
            stubs.SavingsAccountManager.GetSavingsAccountForUser(Arg.Any<Guid>()).Returns(expectedResponse);
            var controller = GetSystemUnderTest(stubs);

            // Act
            var result = await controller.GetSavingsAccountsForUser(userId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var response = result as ObjectResult;
            var responseBody = response?.Value as GetSavingsAccountForUserResponse;

            responseBody.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public async Task AddSavingsAccountForUser_GivenUserIdAndAccountDetails_ShouldReturnAcceptedResult()
        {
            // Arrange
            var stubs = GetStubs();
            var controller = GetSystemUnderTest(stubs);

            var request = new AddSavingsAccountRequest
            {
                Name = "Emergency savings",
                SavingsTarget = 150000,
                SavingsValue = 50107,
                CountryCurrencyCode = "ZAR"
            };

            // Act
            var result = await controller.AddSavingsAccountForUser(Guid.NewGuid(), request);

            // Assert
            Assert.IsInstanceOf<AcceptedResult>(result);
        }

        [Test]
        public async Task AddAmountToSavingsAccountForUser_GivenUserIdAndSavingsAccountIdAndAmount_ShouldOkResult()
        {
            // Arrange
            var stubs = GetStubs();
            var controller = GetSystemUnderTest(stubs);

            var request = new AddAmountToSavingsAccountRequest
            {
                Amount = 2500
            };

            // Act
            var result = await controller.AddAmountToSavingsAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid(),
                request);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task SubtractAmountFromSavingsAccountForUser_GivenUserIdAndSavingsAccountIdAndAmount_ShouldReturnOkResult()
        {
            // Arrange
            var stubs = GetStubs();
            var controller = GetSystemUnderTest(stubs);

            var request = new SubtractAmountFromSavingsAccountRequest
            {
                Amount = 2500
            };

            // Act
            var result = await controller.SubtractAmountFromSavingsAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid(),
                request);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task DeleteSavingsAccountForUser_GivenUserIdAndSavingsAccountId_ShouldOkResult()
        {
            // Arrange
            var stubs = GetStubs();
            var controller = GetSystemUnderTest(stubs);

            // Act
            var result = await controller.DeleteSavingsAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid());
            
            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task UpdateSavingsAccountForUser_GivenUserIdAndSavingsAccountIdAndUpdateRequest_ShouldReturnOkResult()
        {
            // Arrange
            var stubs = GetStubs();
            var controller = GetSystemUnderTest(stubs);

            var request = new UpdateSavingsAccountRequest
            {
                Name = "Notice savings account",
                CountryCurrencyCode = "ZAR",
                CurrentSavingsAmount = 50107,
                TargetSavingsAmount = 100000
            };

            // Act
            var result = await controller.UpdateSavingsAccountForUser(
                Guid.NewGuid(),
                Guid.NewGuid(),
                request);
            
            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}