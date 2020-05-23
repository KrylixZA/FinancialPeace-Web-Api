using System;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Models.Requests.Currencies;
using FinancialPeace.Web.Api.Models.Responses.Currencies;
using FinancialPeace.Web.Api.Repositories;

namespace FinancialPeace.Web.Api.Managers
{
    /// <inheritdoc />
    public class CurrenciesManager : ICurrenciesManager
    {
        private readonly ICurrenciesRepository _currenciesRepository;

        /// <summary>
        /// Creates a new instance of the Currencies Manager class.
        /// </summary>
        /// <param name="currenciesRepository">The currencies repository.</param>
        public CurrenciesManager(ICurrenciesRepository currenciesRepository)
        {
            _currenciesRepository = currenciesRepository ?? throw new ArgumentNullException(nameof(currenciesRepository));
        }
        
        /// <inheritdoc />
        public async Task<GetCurrencyResponse> GetCurrencies()
        {
            var currencies = await _currenciesRepository.GetCurrencies();
            return new GetCurrencyResponse
            {
                Currencies = currencies
            };
        }

        /// <inheritdoc />
        public Task AddCurrency(AddCurrencyRequest request)
        {
            return _currenciesRepository.AddCurrency(request);
        }
    }
}