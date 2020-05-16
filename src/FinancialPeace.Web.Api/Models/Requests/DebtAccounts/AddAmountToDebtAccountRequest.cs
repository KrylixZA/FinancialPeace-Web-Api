using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models.Requests.DebtAccounts
{
    /// <summary>
    /// Represents a request to add an amount to a savings account.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AddAmountToDebtAccountRequest
    {
        /// <summary>
        /// The amount of money that will be added to the debt account in the account's currency.
        /// </summary>
        [Required]
        [JsonProperty("amount", Required = Required.Always)]
        public double Amount { get; set; }
    }
}