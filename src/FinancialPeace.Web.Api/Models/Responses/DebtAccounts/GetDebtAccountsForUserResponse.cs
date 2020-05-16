using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models.Responses.DebtAccounts
{
    /// <summary>
    /// Represents a collection of debt accounts owned by a user.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GetDebtAccountsForUserResponse
    {
        /// <summary>
        /// The user's unique identifier.
        /// </summary>
        [Required]
        [JsonProperty("userId", Required = Required.Always)]
        public Guid UserId { get; set; }
        
        /// <summary>
        /// An enumeration of debt accounts linked to the user.
        /// </summary>
        [JsonProperty("debtAccounts", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<DebtAccount>? DebtAccounts { get; set; }
    }
}