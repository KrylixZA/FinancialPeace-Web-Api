using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models.Responses.Budgets
{
    /// <summary>
    /// Represents a collection of expenses for a user, forming a budget.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GetBudgetForUserResponse
    {
        /// <summary>
        /// The user's unique identifier.
        /// </summary>
        [Required]
        [JsonProperty("userId", Required = Required.Always)]
        public Guid UserId { get; set; }

        /// <summary>
        /// The user's expenses.
        /// </summary>
        [JsonProperty("expenses", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<Expense>? Expenses { get; set; }
    }
}