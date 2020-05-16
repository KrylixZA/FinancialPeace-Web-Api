using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models.Requests.Budgets
{
    /// <summary>
    /// Represents a request to update an expense details for a user.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class UpdateExpenseRequest
    {
        /// <summary>
        /// The expense category name.
        /// </summary>
        [JsonProperty("expenseCategoryName")]
        public string? ExpenseCategoryName { get; set; }
        
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