using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models.Requests.SavingsAccounts
{
    /// <summary>
    /// Represents a request to update a savings account details.
    /// </summary>
    public class UpdateSavingsAccountRequest
    {
        /// <summary>
        /// The country's currency code, such as "ZAR" or "USD".
        /// </summary>
        [JsonProperty("countryCurrencyCode")]
        public string? CountryCurrencyCode { get; set; }

        /// <summary>
        /// The current savings account balance.
        /// </summary>
        [JsonProperty("currentSavingsAmount")]
        public double? CurrentSavingsAmount { get; set; }

        /// <summary>
        /// The target savings account balance.
        /// </summary>
        [JsonProperty("targetSavingsAmount")]
        public double? TargetSavingsAmount { get; set; }

        /// <summary>
        /// The savings account display friendly name.
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}