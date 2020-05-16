using Shared.WebApi.Core.Errors;

namespace FinancialPeace.Web.Api.Errors
{
    /// <summary>
    /// An implementation of the error message selector.
    /// </summary>
    public class ErrorMessageSelector : IErrorMessageSelector
    {
        private const string GeneralErrorMessage = "A general error occurred against which no details can be collected.";
        
        /// <inheritdoc />
        public string GetErrorMessage(int errorCode)
        {
            switch (errorCode)
            {
                default:
                case (int)ErrorCodes.GeneralError:
                    return GeneralErrorMessage;
            }
        }
    }
}