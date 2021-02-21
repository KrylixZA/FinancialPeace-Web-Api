using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FinancialPeace.Web.Api.Repositories.Connection;
using NSubstitute;
using NUnit.Framework;

namespace FinancialPeace.Web.Api.Tests.Repositories.Connection
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class SqlConnectionWrapperTests
    {
        private struct Stubs
        {
            public IDbConnection DbConnection { get; set; }
        }

        private static Stubs GetStubs()
        {
            var stubs = new Stubs
            {
                DbConnection = Substitute.For<IDbConnection>()
            };

            return stubs;
        }

        private static SqlConnectionWrapper GetSystemUnderTest(Stubs stubs)
        {
            return new SqlConnectionWrapper(stubs.DbConnection);
        }

        [Test]
        public void SqlConnectionWrapper_GivenAllParams_ShouldCreateNewInstance()
        {
            // Arrange
            var stubs = GetStubs();

            // Act
            var connectionWrapper = new SqlConnectionWrapper(stubs.DbConnection);

            // Assert
            Assert.IsNotNull(connectionWrapper);
        }

        [TestCase(IsolationLevel.Chaos)]
        [TestCase(IsolationLevel.Serializable)]
        [TestCase(IsolationLevel.Snapshot)]
        [TestCase(IsolationLevel.Unspecified)]
        [TestCase(IsolationLevel.ReadCommitted)]
        [TestCase(IsolationLevel.ReadUncommitted)]
        [TestCase(IsolationLevel.RepeatableRead)]
        public void BeginTransaction_GivenAnIsolationLevel_ShouldReturnExpectedTransaction(
            IsolationLevel isolationLevel)
        {
            // Arrange
            var stubs = GetStubs();
            var expectedTrans = Substitute.For<IDbTransaction>();
            stubs.DbConnection.BeginTransaction(Arg.Any<IsolationLevel>()).Returns(expectedTrans);
            var connectionWrapper = GetSystemUnderTest(stubs);

            // Act
            var actual = connectionWrapper.BeginTransaction(isolationLevel);

            // Assert
            Assert.AreEqual(expectedTrans, actual);
        }
    }
}