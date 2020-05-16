using System.Diagnostics.CodeAnalysis;
using FinancialPeace.Web.Api.Errors;
using NUnit.Framework;

namespace FinancialPeace.Web.Api.Tests.Errors
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class ErrorMessageSelectorTests
    {
        private static ErrorMessageSelector GetSystemUnderTest()
        {
            return new ErrorMessageSelector();
        }

        [TestCase(int.MinValue)]
        [TestCase(0)]
        [TestCase(int.MaxValue)]
        public void GetErrorMessage_GivenUnknownErrorCode_ShouldReturnGeneralErrorMessage(int errorCode)
        {
            // Arrange
            const string expectedMessage = "A general error occurred against which no details can be collected.";
            
            var messageSelector = GetSystemUnderTest();

            // Act
            var actualMessage = messageSelector.GetErrorMessage(errorCode);

            // Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }
        
        [Test]
        public void GetErrorMessage_GivenGeneralErrorCode_ShouldReturnGeneralErrorMessage()
        {
            // Arrange
            const string expectedMessage = "A general error occurred against which no details can be collected.";
            
            const int errorCode = (int) ErrorCodes.GeneralError;
            var messageSelector = GetSystemUnderTest();

            // Act
            var actualMessage = messageSelector.GetErrorMessage(errorCode);

            // Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }
    }
}