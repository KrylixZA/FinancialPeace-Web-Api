using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.Currencies;
using FinancialPeace.Web.Api.Repositories.Connection;
using Microsoft.Extensions.Logging;

namespace FinancialPeace.Web.Api.Repositories
{
    /// <inheritdoc />
    public class CurrenciesRepository : ICurrenciesRepository
    {
        private const string GetCurrenciesProc = "Freedom.pr_GetCurrencies";
        private const string CreateCurrencyProc = "Freedom.pr_CreateCurrency";
        
        private readonly ISqlConnectionProvider _sqlConnectionProvider;
        private readonly ILogger<CurrenciesRepository> _logger;

        /// <summary>
        /// Creates a new instance of the Currencies Repository class.
        /// </summary>
        /// <param name="sqlConnectionProvider">The SQL connection provider.</param>
        /// <param name="logger">The logger.</param>
        public CurrenciesRepository(
            ISqlConnectionProvider sqlConnectionProvider,
            ILogger<CurrenciesRepository> logger)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
            _logger = logger;
        }
        
        /// <inheritdoc />
        public async Task<IEnumerable<Currency>> GetCurrenciesAsync()
        {
            _logger.LogInformation($"GetCurrenciesAsync start");
            using var conn = _sqlConnectionProvider.Open();
            var response = await conn.QueryAsync<Currency>(GetCurrenciesProc, commandType: CommandType.StoredProcedure);
            _logger.LogInformation($"GetCurrenciesAsync end");
            return response;
        }

        /// <inheritdoc />
        public async Task AddCurrencyAsync(AddCurrencyRequest request)
        {
            _logger.LogInformation($"AddCurrencyAsync start");
            using var conn = _sqlConnectionProvider.Open();
            using var trans = conn.BeginTransaction();
            var parameters = new DynamicParameters();
            parameters.Add("$country", request.Country);
            parameters.Add("$name", request.Name);
            parameters.Add("$countryCurrencyCode", request.CountryCurrencyCode);
            parameters.Add("$randExchangeRate", request.RandExchangeRate);
            await conn.ExecuteNonQueryAsync(CreateCurrencyProc, parameters, trans, commandType: CommandType.StoredProcedure);
            trans.Commit();
            _logger.LogInformation($"AddCurrencyAsync end");
        }
    }
}