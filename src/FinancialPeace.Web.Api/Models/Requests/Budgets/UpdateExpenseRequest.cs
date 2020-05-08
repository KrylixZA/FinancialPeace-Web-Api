using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models.Requests.Budgets
{
    /// <summary>
    /// Represents a request to update an expense details for a user.
    /// </summary>
    public class UpdateExpenseRequest
    {
        /// <summary>
        /// The expense display name.
        /// </summary>
        [JsonProperty("displayName")]
        public string? DisplayName { get; set; }
        
        /// <summary>
        /// The country's currency code, such as "ZAR" or "USD".
        /// </summary>
        [JsonProperty("countryCurrencyCode")]
        public string? CountryCurrencyCode { get; set; }
        
        /// <summary>
        /// The value of the expense.
        /// </summary>
        [JsonProperty("value")]
        public double? Value { get; set; }
    }
}