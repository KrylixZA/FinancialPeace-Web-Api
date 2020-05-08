using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models.Requests.SavingsAccounts
{
    /// <summary>
    /// Represents a request to add an amount to a savings account.
    /// </summary>
    public class AddAmountToSavingsAccountRequest
    {
        /// <summary>
        /// The amount of money that will be added to the savings account in the account's currency.
        /// </summary>
        [Required]
        [JsonProperty("amount", Required = Required.Always)]
        public double Amount { get; set; }
    }
}