using System;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models.Requests.DebtAccounts
{
    /// <summary>
    /// Represents a request to update a debt account details.
    /// </summary>
    public class UpdateDebtAccountRequest
    {
        /// <summary>
        /// The country's currency code, such as "ZAR" or "USD".
        /// </summary>
        [JsonProperty("countryCurrencyCode")]
        public string? CountryCurrencyCode { get; set; }

        /// <summary>
        /// The current amount owed on the debt account.
        /// </summary>
        [JsonProperty("currentAmountOwed")]
        public double? CurrentAmountOwed { get; set; }

        /// <summary>
        /// The targeted date to pay off the debt.
        /// </summary>
        [JsonProperty("targetPayoffDate")]
        public DateTime? TargetPayoffDate { get; set; }
        
        /// <summary>
        /// The actual date the debt was paid off.
        /// </summary>
        [JsonProperty("actualPayoffDate")]
        public DateTime? ActualPayoffDate { get; set; }

        /// <summary>
        /// The debt account display friendly name.
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}