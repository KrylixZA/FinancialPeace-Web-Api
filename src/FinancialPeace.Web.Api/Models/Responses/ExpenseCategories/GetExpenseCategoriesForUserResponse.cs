using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models.Responses.ExpenseCategories
{
    /// <summary>
    /// Represents a collection of expense categories mapped to a user.
    /// </summary>
    public class GetExpenseCategoriesForUserResponse
    {
        /// <summary>
        /// The user's unique identifier.
        /// </summary>
        [Required]
        [JsonProperty("userId", Required = Required.Always)]
        public Guid UserId { get; set; }

        /// <summary>
        /// A collection of expense categories linked to the user.
        /// </summary>
        [JsonProperty("expenseCategories")]
        public IEnumerable<ExpenseCategory> ExpenseCategories { get; set; }
    }
}