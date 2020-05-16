using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models.Responses.Currencies
{
    /// <summary>
    /// Represents a collection of available currencies.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GetCurrencyResponse
    {
        /// <summary>
        /// An enumeration of currencies. This could be empty if there are no currencies recorded in the database.
        /// </summary>
        [JsonProperty("currencies", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<Currency>? Currencies { get; set; }
    }
}