using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models
{
    /// <summary>
    /// Represents an instance of an expense category.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ExpenseCategory
    {
        /// <summary>
        /// The unique identifier associated to the expense category.
        /// </summary>
        [Required]
        [JsonProperty("expenseCategoryId", Required = Required.Always)]
        public Guid ExpenseCategoryId { get; set; }
        
        /// <summary>
        /// The name of the expense category.
        /// </summary>
        [Required]
        [JsonProperty("expenseCategoryName", Required = Required.Always)]
        public string ExpenseCategoryName { get; set; } = null!;
    }
}