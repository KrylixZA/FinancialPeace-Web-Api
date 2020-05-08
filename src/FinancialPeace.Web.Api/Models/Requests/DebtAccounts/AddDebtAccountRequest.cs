using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models.Requests.DebtAccounts
{
    /// <summary>
    /// Represents a request to create a debt account.
    /// </summary>
    public class AddDebtAccountRequest
    {
        /// <summary>
        /// The country's currency code, such as "ZAR" or "USD".
        /// </summary>
        [Required]
        [JsonProperty("countryCurrencyCode")]
        public string CountryCurrencyCode { get; set; }

        /// <summary>
        /// The amount owed on the debt at the time of creation within this system.
        /// </summary>
        [Required]
        [JsonProperty("amountOwed", Required = Required.Always)]
        public double AmountOwed { get; set; }

        /// <summary>
        /// The target payoff date of the debt - either the standard loan payoff date or a date closer than that of your choice.
        /// </summary>
        [Required]
        [JsonProperty("targetPayoffDate", Required = Required.Always)]
        public DateTime TargetPayoffDate { get; set; }

        /// <summary>
        /// The display friendly name of the debt account.
        /// </summary>
        [Required]
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }
    }
}