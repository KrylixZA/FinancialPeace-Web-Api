using System;

namespace FinancialPeace.Web.Api.Repositories.Connection
{
    /// <inheritdoc />
    public class SqlConnectionProvider : ISqlConnectionProvider
    {
        private readonly ISqlConnectionWrapper _sqlConnectionWrapper;

        /// <summary>
        /// Initializes a new instance of the SqlConnectionProvider class.
        /// </summary>
        /// <param name="sqlConnectionWrapper">The SQL connection wrapper.</param>
        public SqlConnectionProvider(ISqlConnectionWrapper sqlConnectionWrapper)
        {
            _sqlConnectionWrapper = sqlConnectionWrapper;
        }

        /// <inheritdoc />
        public ISqlConnectionWrapper Open()
        {
            return _sqlConnectionWrapper;
        }
    }
}