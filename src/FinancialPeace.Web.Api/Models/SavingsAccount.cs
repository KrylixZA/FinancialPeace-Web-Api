using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models
{
    /// <summary>
    /// Represents an instance of a savings account for a user.
    /// </summary>
    public class SavingsAccount
    {
        /// <summary>
        /// The savings account unique identifier.
        /// </summary>
        [Required]
        [JsonProperty("savingsAccountId", Required = Required.Always)]
        public Guid SavingsAccountId { get; set; }

        /// <summary>
        /// The country's currency code, such as "ZAR" or "USD".
        /// </summary>
        [Required]
        [JsonProperty("countryCurrencyCode", Required = Required.Always)]
        public string CountryCurrencyCode { get; set; }

        /// <summary>
        /// The savings account initial value.
        /// </summary>
        [Required]
        [JsonProperty("initialSavingsValue", Required = Required.Always)]
        public double InitialSavingsValue { get; set; }

        /// <summary>
        /// The savings account current value.
        /// </summary>
        [Required]
        [JsonProperty("currentSavingsValue", Required = Required.Always)]
        public double CurrentSavingsValue { get; set; }

        /// <summary>
        /// The target value of the savings account.
        /// </summary>
        [JsonProperty("savingsTarget")]
        public double SavingsTarget { get; set; }

        /// <summary>
        /// A display friendly name for the savings account, such "Emergency Savings".
        /// </summary>
        [Required]
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }
    }
}