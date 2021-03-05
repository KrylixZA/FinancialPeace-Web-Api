using System.Diagnostics.CodeAnalysis;
using FinancialPeace.Web.Api.Repositories.Connection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace FinancialPeace.Web.Api.Tests.Repositories.Connection
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class SqlConnectionProviderTests
    {
        private struct Stubs
        {
            public ISqlConnectionWrapper SqlConnectionWrapper { get; set; }
            public static ILogger<SqlConnectionProvider> Logger => Substitute.For<ILogger<SqlConnectionProvider>>();
        }

        private static Stubs GetStubs()
        {
            var stubs = new Stubs
            {
                SqlConnectionWrapper = Substitute.For<ISqlConnectionWrapper>()
            };

            return stubs;
        }

        private static SqlConnectionProvider GetSystemUnderTest(Stubs stubs)
        {
            return new SqlConnectionProvider(stubs.SqlConnectionWrapper, Stubs.Logger);
        }

        [Test]
        public void Open_GivenDbConnectionWrapper_ShouldReturnSameDbWrapper()
        {
            // Arrange
            var stubs = GetStubs();
            var connectionProvider = GetSystemUnderTest(stubs);

            // Act
            var conn = connectionProvider.Open();

            // Assert
            Assert.AreEqual(stubs.SqlConnectionWrapper, conn);
        }
    }
}