using System;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models.Requests.Currencies
{
    /// <summary>
    /// Represents a request to create a new currency.
    /// </summary>
    public class AddCurrencyRequest : Currency
    {
        /// <summary>
        /// The unique identifier associated to the currency.
        /// </summary>
        [JsonIgnore]
        public new Guid CurrencyId => Guid.Empty;
    }
}