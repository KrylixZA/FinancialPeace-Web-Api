using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Models;
using FinancialPeace.Web.Api.Models.Requests.Currencies;

namespace FinancialPeace.Web.Api.Repositories
{
    /// <summary>
    /// Defines a contract for interacting with currencies in the database.
    /// </summary>
    public interface ICurrenciesRepository
    {
        /// <summary>
        /// Gets all currently listed currencies from the database.
        /// </summary>
        /// <returns>An enumeration of currencies.</returns>
        Task<IEnumerable<Currency>> GetCurrencies();

        /// <summary>
        /// Attempts to persist a new currency to the database.
        /// </summary>
        /// <param name="request">The details of the currency to be added.</param>
        Task AddCurrency(AddCurrencyRequest request);
    }
}