using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FinancialPeace.Web.Api.Models.Responses.SavingsAccounts
{
    /// <summary>
    /// Represents a collection of savings accounts owned by a user.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GetSavingsAccountForUserResponse
    {
        /// <summary>
        /// The user's unique identifier.
        /// </summary>
        [Required]
        [JsonProperty("userId", Required = Required.Always)]
        public Guid UserId { get; set; }
        
        /// <summary>
        /// An enumeration of savings accounts linked to the user.
        /// </summary>
        [JsonProperty("savingsAccounts", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<SavingsAccount>? SavingsAccounts { get; set; }
    }
}