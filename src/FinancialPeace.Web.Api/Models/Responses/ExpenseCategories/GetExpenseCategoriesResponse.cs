using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models.Responses.ExpenseCategories
{
    /// <summary>
    /// Represents a collection of available expense categories.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GetExpenseCategoriesResponse
    {
        /// <summary>
        /// An enumeration of expense categories.
        /// </summary>
        [JsonProperty("expenseCategories", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<ExpenseCategory>? ExpenseCategories { get; set; }
    }
}