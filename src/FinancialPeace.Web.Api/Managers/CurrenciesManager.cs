using System.Threading.Tasks;
using FinancialPeace.Web.Api.Models.Requests.Currencies;
using FinancialPeace.Web.Api.Models.Responses.Currencies;
using FinancialPeace.Web.Api.Repositories;
using Microsoft.Extensions.Logging;

namespace FinancialPeace.Web.Api.Managers
{
    /// <inheritdoc />
    public class CurrenciesManager : ICurrenciesManager
    {
        private readonly ICurrenciesRepository _currenciesRepository;
        private readonly ILogger<CurrenciesManager> _logger;

        /// <summary>
        /// Creates a new instance of the Currencies Manager class.
        /// </summary>
        /// <param name="currenciesRepository">The currencies repository.</param>
        /// <param name="logger">The logger.</param>
        public CurrenciesManager(
            ICurrenciesRepository currenciesRepository,
            ILogger<CurrenciesManager> logger)
        {
            _currenciesRepository = currenciesRepository;
            _logger = logger;
        }
        
        /// <inheritdoc />
        public async Task<GetCurrencyResponse> GetCurrenciesAsync()
        {
            _logger.LogInformation("GetCurrenciesAsync start");
            var currencies = await _currenciesRepository.GetCurrenciesAsync();
            _logger.LogInformation("GetCurrenciesAsync end");
            return new GetCurrencyResponse
            {
                Currencies = currencies
            };
        }

        /// <inheritdoc />
        public Task AddCurrency(AddCurrencyRequest request)
        {
            _logger.LogInformation("AddCurrency start");
            var responseTask = _currenciesRepository.AddCurrencyAsync(request);
            _logger.LogInformation("AddCurrency end");
            return responseTask;
        }
    }
}