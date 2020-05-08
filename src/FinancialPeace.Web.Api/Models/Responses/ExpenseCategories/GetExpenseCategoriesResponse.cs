using System.Collections.Generic;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models.Responses.ExpenseCategories
{
    /// <summary>
    /// Represents a collection of available expense categories.
    /// </summary>
    public class GetExpenseCategoriesResponse
    {
        /// <summary>
        /// An enumeration of expense categories.
        /// </summary>
        [JsonProperty("expenseCategories")]
        public IEnumerable<ExpenseCategory> ExpenseCategories { get; set; }
    }
}