using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Dapper;

namespace FinancialPeace.Web.Api.Helpers
{
    /// <summary>
    /// Provides a helper to handle MySQL unique identifiers.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MySqlGuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        /// <summary>
        /// Set's the value of the guid.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="value">The globally unique identifier.</param>
        public override void SetValue(IDbDataParameter parameter, Guid value)
        {
            parameter.Value = value.ToString();
        }

        /// <summary>
        /// Parses the string to a C# Guid.
        /// </summary>
        /// <param name="value">The string value.</param>
        public override Guid Parse(object value)
        {
            return new Guid((string)value);
        }
    }
}