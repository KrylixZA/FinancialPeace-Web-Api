using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Controllers.Currencies;
using FinancialPeace.Web.Api.Managers;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.Currencies;
using FinancialPeace.Web.Api.Models.Responses.Currencies;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;

namespace FinancialPeace.Web.Api.Tests.Controllers.Currencies
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class CurrenciesControllerTests
    {
        private struct Stubs
        {
            public ICurrenciesManager CurrenciesManager { get; set; }
        }

        private static Stubs GetStubs()
        {
            var stubs = new Stubs
            {
                CurrenciesManager = Substitute.For<ICurrenciesManager>()
            };

            return stubs;
        }

        private static CurrenciesController GetSystemUnderTest(Stubs stubs)
        {
            return new CurrenciesController(stubs.CurrenciesManager);
        }

        [Test]
        public void CurrenciesController_GivenAllParams_ShouldCreateNewInstance()
        {
            // Arrange
            var stubs = GetStubs();

            // Act
            var controller = new CurrenciesController(stubs.CurrenciesManager);

            // Assert
            Assert.IsNotNull(controller);
        }

        [Test]
        public async Task GetCurrencies_OnRequest_ShouldReturnOkObjectResult()
        {
            // Arrange
            var expectedResponse = new GetCurrencyResponse
            {
                Currencies = new List<Currency>
                {
                    new Currency
                    {
                        Country = "South Africa",
                        Name = "South African Rand",
                        CurrencyId = Guid.NewGuid(),
                        CountryCurrencyCode = "ZAR",
                        RandExchangeRate = 1.0
                    }
                }
            };

            var stubs = GetStubs();
            stubs.CurrenciesManager.GetCurrencies().Returns(expectedResponse);
            var controller = GetSystemUnderTest(stubs);

            // Act
            var result = await controller.GetCurrencies();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var response = result as ObjectResult;
            var responseBody = response?.Value as GetCurrencyResponse;

            responseBody.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public async Task AddCurrency_GivenAddCurrencyRequest_ShouldReturnAcceptedResponse()
        {
            // Arrange
            var stubs = GetStubs();
            var controller = GetSystemUnderTest(stubs);

            var request = new AddCurrencyRequest
            {
                Country = "South Africa",
                Name = "South African Rand",
                CountryCurrencyCode = "ZAR",
                RandExchangeRate = 1.0
            };

            // Act
            var result = await controller.AddCurrency(request);

            // Assert
            Assert.IsInstanceOf<AcceptedResult>(result);
        }
    }
}