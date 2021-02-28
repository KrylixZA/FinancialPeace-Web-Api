using System;
using Microsoft.Extensions.Logging;

namespace FinancialPeace.Web.Api.Repositories.Connection
{
    /// <inheritdoc />
    public class SqlConnectionProvider : ISqlConnectionProvider
    {
        private readonly ISqlConnectionWrapper _sqlConnectionWrapper;
        private readonly ILogger<SqlConnectionProvider> _logger;

        /// <summary>
        /// Initializes a new instance of the SqlConnectionProvider class.
        /// </summary>
        /// <param name="sqlConnectionWrapper">The SQL connection wrapper.</param>
        /// <param name="logger">The logger.</param>
        public SqlConnectionProvider(
            ISqlConnectionWrapper sqlConnectionWrapper,
            ILogger<SqlConnectionProvider> logger)
        {
            _sqlConnectionWrapper = sqlConnectionWrapper;
            _logger = logger;
        }

        /// <inheritdoc />
        public ISqlConnectionWrapper Open()
        {
            _logger.LogInformation("Open called");
            return _sqlConnectionWrapper;
        }
    }
}