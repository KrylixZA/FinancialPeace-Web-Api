using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models
{
    /// <summary>
    /// Represents an instance of a debt account for a user.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DebtAccount
    {
        /// <summary>
        /// The debt account unique identifier.
        /// </summary>
        [Required]
        [JsonProperty("debtAccountId", Required = Required.Always)]
        public Guid DebtAccountId { get; set; }

        /// <summary>
        /// The country's currency code, such as "ZAR" or "USD".
        /// </summary>
        [Required]
        [JsonProperty("countryCurrencyCode", Required = Required.Always)]
        public string CountryCurrencyCode { get; set; } = null!;

        /// <summary>
        /// The initial amount owed upon creation within this system.
        /// </summary>
        [Required]
        [JsonProperty("initialAmountOwed", Required = Required.Always)]
        public double InitialAmountOwed { get; set; }

        /// <summary>
        /// The current amount owed.
        /// </summary>
        [Required]
        [JsonProperty("currentAmountOwed", Required = Required.Always)]
        public double CurrentAmountOwed { get; set; }

        /// <summary>
        /// The target payoff date.
        /// </summary>
        [Required]
        [JsonProperty("targetPayoffDate", Required = Required.Always)]
        public DateTime TargetPayoffDate { get; set; }

        /// <summary>
        /// The actual payoff date, if already paid off.
        /// </summary>
        [Required]
        [JsonProperty("actualPayoffDate", Required = Required.Always)]
        public DateTime? ActualPayoffDate { get; set; }

        /// <summary>
        /// The display friendly name of the debt account.
        /// </summary>
        [Required]
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; } = null!;
    }
}