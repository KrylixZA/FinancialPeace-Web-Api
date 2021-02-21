using System;
using System.Diagnostics.CodeAnalysis;
using FinancialPeace.Web.Api.Repositories.Connection;
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
            return new SqlConnectionProvider(stubs.SqlConnectionWrapper);
        }

        [Test]
        public void SqlConnectionProvider_GivenAllParams_ShouldCreateNewInstance()
        {
            // Arrange
            var stubs = GetStubs();

            // Act
            var connectionProvider = new SqlConnectionProvider(stubs.SqlConnectionWrapper);

            // Assert
            Assert.IsNotNull(connectionProvider);
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