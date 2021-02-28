using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Managers;
using FinancialPeace.Web.Api.Models.Requests.DebtAccounts;
using FinancialPeace.Web.Api.Models.Responses.DebtAccounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.WebApi.Core.Errors;

namespace FinancialPeace.Web.Api.Controllers.DebtAccounts
{
    /// <summary>
    /// Exposes functionality to manage a user's debt accounts.
    /// </summary>
    [Authorize]
    [Route("debtAccounts")]
    public class DebtAccountsController : ControllerBase
    {
        private readonly IDebtAccountsManager _debtAccountsManager;
        private readonly ILogger<DebtAccountsController> _logger;

        /// <summary>
        /// Creates a new instance of the Debt Accounts Controller.
        /// </summary>
        /// <param name="debtAccountsManager">The debt accounts manager.</param>
        /// <param name="logger">The logger.</param>
        public DebtAccountsController(
            IDebtAccountsManager debtAccountsManager,
            ILogger<DebtAccountsController> logger)
        {
            _debtAccountsManager = debtAccountsManager;
            _logger = logger;
        }

        /// <summary>
        /// Gets the debt account linked to a user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <response code="200">A collection of debt accounts linked to a user.</response>
        /// <response code="401">The token provided does not provide access to this resource.</response>
        /// <response code="403">The request was forbidden from proceeding.</response>
        /// <response code="500">Something went wrong with processing the request, likely due to environmental issues.</response>
        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(GetDebtAccountsForUserResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetDebtAccountsForUser([Required] [FromRoute] Guid userId)
        {
            _logger.LogInformation($"GetDebtAccountsForUser start. UserId: {userId}");
            var response = await _debtAccountsManager.GetDebtAccountForUserAsync(userId);
            _logger.LogInformation($"GetDebtAccountsForUser end. UserId: {userId}");
            return Ok(response);
        }

        /// <summary>
        /// Persists a new debt account to the database for the given user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="request">The debt account details.</param>
        /// <response code="202">The debt account was successfully created for the user.</response>
        /// <response code="400">The request was badly formed.</response>
        /// <response code="401">The token provided does not provide access to this resource.</response>
        /// <response code="403">The request was forbidden from proceeding.</response>
        /// <response code="500">Something went wrong with processing the request, likely due to environmental issues.</response>
        [HttpPost("user/{userId}")]
        [ProducesResponseType((int) HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddDebtAccountForUser(
            [Required] [FromRoute] Guid userId,
            [Required] [FromBody] AddDebtAccountRequest request)
        {
            _logger.LogInformation($"AddDebtAccountForUser start. UserId: {userId}");
            await _debtAccountsManager.AddDebtAccountForUserAsync(userId, request);
            _logger.LogInformation($"AddDebtAccountForUser end. UserId: {userId}");
            return Accepted();
        }

        /// <summary>
        /// Increases the amount owed on a debt account for a user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="debtAccountId">The debt account unique identifier.</param>
        /// <param name="request">The details of the request.</param>
        /// <response code="200">The debt account was successfully updated.</response>
        /// <response code="400">The request was badly formed.</response>
        /// <response code="401">The token provided does not provide access to this resource.</response>
        /// <response code="403">The request was forbidden from proceeding.</response>
        /// <response code="500">Something went wrong with processing the request, likely due to environmental issues.</response>
        [HttpPatch("user/{userId}/debtAccount/{debtAccountId}/addAmount")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddAmountToDebtAccount(
            [Required] [FromRoute] Guid userId,
            [Required] [FromRoute] Guid debtAccountId,
            [Required] [FromBody] AddAmountToDebtAccountRequest request)
        {
            _logger.LogInformation($"AddAmountToDebtAccount start. UserId: {userId}. DebtAccountId: {debtAccountId}");
            await _debtAccountsManager.AddAmountToDebtAccountForUserAsync(userId, debtAccountId, request);
            _logger.LogInformation($"AddAmountToDebtAccount end. UserId: {userId}. DebtAccountId: {debtAccountId}");
            return Ok();
        }

        /// <summary>
        /// Reduces the amount of money owed on a debt account for a user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="debtAccountId">The debt account unique identifier.</param>
        /// <param name="request">The details of the request.</param>
        /// <response code="200">The debt account was successfully updated.</response>
        /// <response code="400">The request was badly formed.</response>
        /// <response code="401">The token provided does not provide access to this resource.</response>
        /// <response code="403">The request was forbidden from proceeding.</response>
        /// <response code="500">Something went wrong with processing the request, likely due to environmental issues.</response>
        [HttpPatch("user/{userId}/debtAccount/{debtAccountId}/subtractAmount")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> SubtractAmountFromDebtAccountForUser(
            [Required] [FromRoute] Guid userId,
            [Required] [FromRoute] Guid debtAccountId,
            [Required] [FromBody] SubtractAmountFromDebtAccountRequest request)
        {
            _logger.LogInformation($"SubtractAmountFromDebtAccountForUser start. UserId: {userId}. DebtAccountId: {debtAccountId}");
            await _debtAccountsManager.SubtractAmountFromDebtAccountForUserAsync(userId, debtAccountId, request);
            _logger.LogInformation($"SubtractAmountFromDebtAccountForUser end. UserId: {userId}. DebtAccountId: {debtAccountId}");
            return Ok();
        }

        /// <summary>
        /// Attempts to delete a user's debt account from their portfolio.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="debtAccountId">The debt account unique identifier.</param>
        /// <response code="200">The debt account was successfully deleted.</response>
        /// <response code="400">The request was badly formed.</response>
        /// <response code="401">The token provided does not provide access to this resource.</response>
        /// <response code="403">The request was forbidden from proceeding.</response>
        /// <response code="500">Something went wrong with processing the request, likely due to environmental issues.</response>
        [HttpDelete("user/{userId}/debtAccount/{debtAccountId}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteDebtAccountForUser(
            [Required] [FromRoute] Guid userId,
            [Required] [FromRoute] Guid debtAccountId)
        {
            _logger.LogInformation($"DeleteDebtAccountForUser start. UserId: {userId}. DebtAccountId: {debtAccountId}");
            await _debtAccountsManager.DeleteDebtAccountForUserAsync(userId, debtAccountId);
            _logger.LogInformation($"DeleteDebtAccountForUser end. UserId: {userId}. DebtAccountId: {debtAccountId}");
            return Ok();
        }

        /// <summary>
        /// Updates the debt account details for a user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="debtAccountId">The debt account unique identifier.</param>
        /// <param name="request">The details of the request.</param>
        /// <response code="200">The debt account was successfully updated.</response>
        /// <response code="400">The request was badly formed.</response>
        /// <response code="401">The token provided does not provide access to this resource.</response>
        /// <response code="403">The request was forbidden from proceeding.</response>
        /// <response code="500">Something went wrong with processing the request, likely due to environmental issues.</response>
        [HttpPatch("user/{userId}/debtAccount/{debtAccountId}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateDebtAccountForUser(
            [Required] [FromRoute] Guid userId,
            [Required] [FromRoute] Guid debtAccountId,
            [Required] [FromBody] UpdateDebtAccountRequest request)
        {
            _logger.LogInformation($"UpdateDebtAccountForUser start. UserId: {userId}. DebtAccountId: {debtAccountId}");
            await _debtAccountsManager.UpdateDebtAccountForUserAsync(userId, debtAccountId, request);
            _logger.LogInformation($"UpdateDebtAccountForUser end. UserId: {userId}. DebtAccountId: {debtAccountId}");
            return Ok();
        }
    }
}