namespace FinancialPeace.Web.Api.Repositories.Connection
{
    /// <summary>
    /// Provides an interface to resolve a connection 
    /// </summary>
    public interface ISqlConnectionProvider
    {
        /// <summary>
        /// Opens a connection to the Freedom database.
        /// </summary>
        ISqlConnectionWrapper Open();
    }
}