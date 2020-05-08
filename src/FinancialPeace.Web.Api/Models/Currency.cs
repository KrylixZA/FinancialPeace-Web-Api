using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models
{
    /// <summary>
    /// A model representation of a currency and it's relevant exchange rate.
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// The unique identifier associated to the currency.
        /// </summary>
        [Required]
        [JsonProperty("currencyId", Required = Required.Always)]
        public Guid CurrencyId { get; set; }

        /// <summary>
        /// The country's currency code, such as "ZAR" or "USD".
        /// </summary>
        [Required]
        [JsonProperty("countryCurrencyCode", Required = Required.Always)]
        public string CountryCurrencyCode { get; set; }
        
        /// <summary>
        /// The name of the currency, such as US Dollar or South African Rand.
        /// </summary>
        [Required]
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        /// <summary>
        /// The country associated to the currency. This can be null, for currencies such as the Euro.
        /// </summary>
        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }
        
        /// <summary>
        /// The currency's exchange rate relative to the US Dollar, such as 19.05.
        /// </summary>
        [Required]
        [JsonProperty("randExchangeRate", Required = Required.Always)]
        public double RandExchangeRate { get; set; }
    }
}