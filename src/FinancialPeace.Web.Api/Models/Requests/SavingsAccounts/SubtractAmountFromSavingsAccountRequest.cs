using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models.Requests.SavingsAccounts
{
    /// <summary>
    /// Represents a request to remove subtract an amount to a savings account.
    /// </summary>
    public class SubtractAmountFromSavingsAccountRequest
    {
        /// <summary>
        /// The amount of money that will be subtracted from the savings account in the account's currency.
        /// </summary>
        [Required]
        [JsonProperty("amount", Required = Required.Always)]
        public double Amount { get; set; }
    }
}