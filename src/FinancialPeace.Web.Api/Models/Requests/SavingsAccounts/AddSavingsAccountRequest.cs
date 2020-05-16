using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models.Requests.SavingsAccounts
{
    /// <summary>
    /// Represents a request to create a savings account.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AddSavingsAccountRequest
    {
        /// <summary>
        /// The country's currency code, such as "ZAR" or "USD".
        /// </summary>
        [Required]
        [JsonProperty("countryCurrencyCode")]
        public string CountryCurrencyCode { get; set; } = null!;

        /// <summary>
        /// The value of the savings account at time of creation within this system.
        /// </summary>
        [Required]
        [JsonProperty("savingsValue", Required = Required.Always)]
        public double SavingsValue { get; set; }

        /// <summary>
        /// The savings target for the account, if any.
        /// </summary>
        [JsonProperty("savingsTarget")]
        public double? SavingsTarget { get; set; }

        /// <summary>
        /// The display friendly name of the savings account.
        /// </summary>
        [Required]
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; } = null!;
    }
}