using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models
{
    /// <summary>
    /// Represents an instance of an expense for a user.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Expense
    {
        /// <summary>
        /// The unique identifier of the budget to which the expense belongs.
        /// </summary>
        [Required]
        [JsonProperty("budgetId", Required = Required.Always)]
        public Guid BudgetId { get; set; }
        
        /// <summary>
        /// The user's unique identifier.
        /// </summary>
        [Required]
        [JsonProperty("userId", Required = Required.Always)]
        public Guid UserId { get; set; }
        
        /// <summary>
        /// The expense unique identifier.
        /// </summary>
        [Required]
        [JsonProperty("expenseId", Required = Required.Always)]
        public Guid ExpenseId { get; set; }
        
        /// <summary>
        /// The expense display name.
        /// </summary>
        [Required]
        [JsonProperty("displayName", Required = Required.Always)]
        public string DisplayName { get; set; } = null!;

        /// <summary>
        /// The country's currency code, such as "ZAR" or "USD".
        /// </summary>
        [Required]
        [JsonProperty("countryCurrencyCode", Required = Required.Always)]
        public string CountryCurrencyCode { get; set; } = null!;

        /// <summary>
        /// The value of the expense.
        /// </summary>
        [Required]
        [JsonProperty("value", Required = Required.Always)]
        public double Value { get; set; }
    }
}