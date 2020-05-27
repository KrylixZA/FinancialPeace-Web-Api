using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.Currencies;
using FinancialPeace.Web.Api.Repositories.Connection;

namespace FinancialPeace.Web.Api.Repositories
{
    /// <inheritdoc />
    public class CurrenciesRepository : ICurrenciesRepository
    {
        private const string GetCurrenciesProc = "Freedom.pr_GetCurrencies";
        private const string CreateCurrencyProc = "Freedom.pr_CreateCurrency";
        
        private readonly ISqlConnectionProvider _sqlConnectionProvider;

        /// <summary>
        /// Creates a new instance of the Currencies Repository class.
        /// </summary>
        /// <param name="sqlConnectionProvider">The SQL connection provider.</param>
        public CurrenciesRepository(ISqlConnectionProvider sqlConnectionProvider)
        {
            _sqlConnectionProvider = sqlConnectionProvider ?? throw new ArgumentNullException(nameof(sqlConnectionProvider));
        }
        
        /// <inheritdoc />
        public async Task<IEnumerable<Currency>> GetCurrencies()
        {
            using var conn = _sqlConnectionProvider.Open();
            return await conn.QueryAsync<Currency>(GetCurrenciesProc, commandType: CommandType.StoredProcedure);
        }

        /// <inheritdoc />
        public async Task AddCurrency(AddCurrencyRequest request)
        {
            using var conn = _sqlConnectionProvider.Open();
            using var trans = conn.BeginTransaction();
            var parameters = new DynamicParameters();
            parameters.Add("$country", request.Country);
            parameters.Add("$name", request.Name);
            parameters.Add("$countryCurrencyCode", request.CountryCurrencyCode);
            parameters.Add("$randExchangeRate", request.RandExchangeRate);
            await conn.ExecuteNonQueryAsync(CreateCurrencyProc, parameters, trans, commandType: CommandType.StoredProcedure);
            trans.Commit();
        }
    }
}