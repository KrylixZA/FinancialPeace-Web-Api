using System;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models.Requests.ExpenseCategories
{
    /// <summary>
    /// Represents a request to create a new expense category
    /// </summary>
    public class AddExpenseCategoryRequest : ExpenseCategory
    {
        /// <summary>
        /// The unique identifier associated to the expense category.
        /// </summary>
        [JsonIgnore]
        public new Guid ExpenseCategoryId => Guid.Empty;
    }
}