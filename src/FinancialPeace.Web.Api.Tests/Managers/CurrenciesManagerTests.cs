using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Managers;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.Currencies;
using FinancialPeace.Web.Api.Models.Responses.Currencies;
using FinancialPeace.Web.Api.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace FinancialPeace.Web.Api.Tests.Managers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class CurrenciesManagerTests
    {
        private struct Stubs
        {
            public ICurrenciesRepository CurrenciesRepository { get; set; }
            public static ILogger<CurrenciesManager> Logger => Substitute.For<ILogger<CurrenciesManager>>();
        }

        private static Stubs GetStubs()
        {
            var stubs = new Stubs
            {
                CurrenciesRepository = Substitute.For<ICurrenciesRepository>()
            };

            return stubs;
        }

        private static CurrenciesManager GetSystemUnderTest(Stubs stubs)
        {
            return new CurrenciesManager(stubs.CurrenciesRepository, Stubs.Logger);
        }

        [Test]
        public async Task GetCurrencies_OnRequest_ShouldReturnExpectedResponse()
        {
            // Arrange
            var currencies = new List<Currency>
            {
                new Currency
                {
                    Country = "South Africa",
                    Name = "South African Rand",
                    CurrencyId = Guid.NewGuid(),
                    CountryCurrencyCode = "ZAR",
                    RandExchangeRate = 1.0
                }
            };
            var expectedResponse = new GetCurrencyResponse
            {
                Currencies = currencies
            };

            var stubs = GetStubs();
            stubs.CurrenciesRepository.GetCurrenciesAsync().Returns(currencies);
            var manager = GetSystemUnderTest(stubs);

            // Act
            var actualResponse = await manager.GetCurrenciesAsync();

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public void AddCurrency_GivenAddCurrencyRequest_ShouldCompleteWithoutError()
        {
            // Arrange
            var stubs = GetStubs();
            var repository = GetSystemUnderTest(stubs);

            var request = new AddCurrencyRequest
            {
                Country = "South Africa",
                Name = "South African Rand",
                CountryCurrencyCode = "ZAR",
                RandExchangeRate = 1.0
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await repository.AddCurrency(request));
        }
    }
}