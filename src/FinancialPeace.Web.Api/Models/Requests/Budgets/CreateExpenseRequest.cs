using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models.Requests.Budgets
{
    /// <summary>
    /// Represents a request to create an expense for a user.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CreateExpenseRequest
    {
        /// <summary>
        /// The name of the expense category. This expense category must exist in the list of expense categories.
        /// </summary>
        [Required]
        [JsonProperty("expenseCategoryName", Required = Required.Always)]
        public string ExpenseCategoryName { get; set; } = null!;

        /// <summary>
        /// The country's currency code, such as "ZAR" or "USD".
        /// </summary>
        [Required]
        [JsonProperty("countryCurrencyCode", Required = Required.Always)]
        public string CountryCurrencyCode { get; set; } = null!;

        /// <summary>
        /// The value associated to the expense.
        /// </summary>
        [Required]
        [JsonProperty("value", Required = Required.Always)]
        public double Value { get; set; }
    }
}