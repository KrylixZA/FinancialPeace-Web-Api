using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Managers;
using FinancialPeace.Web.Api.Models.Requests.SavingsAccounts;
using FinancialPeace.Web.Api.Models.Responses.SavingsAccounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.WebApi.Core.Errors;

namespace FinancialPeace.Web.Api.Controllers.SavingsAccounts
{
    /// <summary>
    /// Exposes functionality to manage a user's savings accounts.
    /// </summary>
    [Authorize]
    [Route("savingsAccounts")]
    public class SavingsAccountController : ControllerBase
    {
        private readonly ISavingsAccountManager _savingsAccountManager;
        private readonly ILogger<SavingsAccountController> _logger;

        /// <summary>
        /// Initializes a new instance of the Savings Account Controller class.
        /// </summary>
        /// <param name="savingsAccountManager">The savings account manager.</param>
        /// <param name="logger">The logger.</param>
        public SavingsAccountController(
            ISavingsAccountManager savingsAccountManager, 
            ILogger<SavingsAccountController> logger)
        {
            _savingsAccountManager = savingsAccountManager;
            _logger = logger;
        }

        /// <summary>
        /// Gets the savings account linked to a user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <response code="200">A collection of savings accounts linked to a user.</response>
        /// <response code="401">The token provided does not provide access to this resource.</response>
        /// <response code="403">The request was forbidden from proceeding.</response>
        /// <response code="500">Something went wrong with processing the request, likely due to environmental issues.</response>
        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(GetSavingsAccountForUserResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetSavingsAccountsForUser([Required] [FromRoute] Guid userId)
        {
            _logger.LogInformation($"GetSavingsAccountsForUser start. UserId: {userId}");
            var response = await _savingsAccountManager.GetSavingsAccountForUserAsync(userId);
            _logger.LogInformation($"GetSavingsAccountsForUser end. UserId: {userId}");
            return Ok(response);
        }

        /// <summary>
        /// Persists a new savings account to the database for the given user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="request">The savings account details.</param>
        /// <response code="202">The savings account was successfully created for the user.</response>
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
        public async Task<IActionResult> AddSavingsAccountForUser(
            [Required] [FromRoute] Guid userId,
            [Required] [FromBody] AddSavingsAccountRequest request)
        {
            _logger.LogInformation($"AddSavingsAccountForUser start. UserId: {userId}");
            await _savingsAccountManager.AddSavingsAccountForUserAsync(userId, request);
            _logger.LogInformation($"AddSavingsAccountForUser end. UserId: {userId}");
            return Accepted();
        }

        /// <summary>
        /// Increases the current balance of a user's savings account.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="savingsAccountId">The savings account unique identifier.</param>
        /// <param name="request">The details of the request.</param>
        /// <response code="200">The savings account was successfully updated.</response>
        /// <response code="400">The request was badly formed.</response>
        /// <response code="401">The token provided does not provide access to this resource.</response>
        /// <response code="403">The request was forbidden from proceeding.</response>
        /// <response code="500">Something went wrong with processing the request, likely due to environmental issues.</response>
        [HttpPatch("user/{userId}/savingsAccount/{savingsAccountId}/addAmount")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddAmountToSavingsAccountForUser(
            [Required] [FromRoute] Guid userId,
            [Required] [FromRoute] Guid savingsAccountId,
            [Required] [FromBody] AddAmountToSavingsAccountRequest request)
        {
            _logger.LogInformation($"AddAmountToSavingsAccountForUser start. UserId: {userId}. SavingsAccountId: {savingsAccountId}");
            await _savingsAccountManager.AddAmountToSavingsAccountForUserAsync(userId, savingsAccountId, request);
            _logger.LogInformation($"AddAmountToSavingsAccountForUser end. UserId: {userId}. SavingsAccountId: {savingsAccountId}");
            return Ok();
        }

        /// <summary>
        /// Reduces the current balance of a user's savings account.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="savingsAccountId">The savings account unique identifier.</param>
        /// <param name="request">The details of the request.</param>
        /// <response code="200">The savings account was successfully updated.</response>
        /// <response code="400">The request was badly formed.</response>
        /// <response code="401">The token provided does not provide access to this resource.</response>
        /// <response code="403">The request was forbidden from proceeding.</response>
        /// <response code="500">Something went wrong with processing the request, likely due to environmental issues.</response>
        [HttpPatch("user/{userId}/savingsAccount/{savingsAccountId}/subtractAmount")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> SubtractAmountFromSavingsAccountForUser(
            [Required] [FromRoute] Guid userId,
            [Required] [FromRoute] Guid savingsAccountId,
            [Required] [FromBody] SubtractAmountFromSavingsAccountRequest request)
        {
            _logger.LogInformation($"SubtractAmountFromSavingsAccountForUser start. UserId: {userId}. SavingsAccountId: {savingsAccountId}");
            await _savingsAccountManager.SubtractAmountFromSavingsAccountForUserAsync(userId, savingsAccountId, request);
            _logger.LogInformation($"SubtractAmountFromSavingsAccountForUser end. UserId: {userId}. SavingsAccountId: {savingsAccountId}");
            return Ok();
        }

        /// <summary>
        /// Attempts to delete a user's savings account from their portfolio.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="savingsAccountId">The savings account unique identifier.</param>
        /// <response code="200">The savings account was successfully deleted.</response>
        /// <response code="400">The request was badly formed.</response>
        /// <response code="401">The token provided does not provide access to this resource.</response>
        /// <response code="403">The request was forbidden from proceeding.</response>
        /// <response code="500">Something went wrong with processing the request, likely due to environmental issues.</response>
        [HttpDelete("user/{userId}/savingsAccount/{savingsAccountId}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteSavingsAccountForUser(
            [Required] [FromRoute] Guid userId,
            [Required] [FromRoute] Guid savingsAccountId)
        {
            _logger.LogInformation($"DeleteSavingsAccountForUser start. UserId: {userId}. SavingsAccountId: {savingsAccountId}");
            await _savingsAccountManager.DeleteSavingsAccountForUserAsync(userId, savingsAccountId);
            _logger.LogInformation($"DeleteSavingsAccountForUser end. UserId: {userId}. SavingsAccountId: {savingsAccountId}");
            return Ok();
        }

        /// <summary>
        /// Updates the savings account details for a user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="savingsAccountId">The savings account unique identifier.</param>
        /// <param name="request">The details of the request.</param>
        /// <response code="200">The savings account was successfully updated.</response>
        /// <response code="400">The request was badly formed.</response>
        /// <response code="401">The token provided does not provide access to this resource.</response>
        /// <response code="403">The request was forbidden from proceeding.</response>
        /// <response code="500">Something went wrong with processing the request, likely due to environmental issues.</response>
        [HttpPatch("user/{userId}/savingsAccount/{savingsAccountId}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateSavingsAccountForUser(
            [Required] [FromRoute] Guid userId,
            [Required] [FromRoute] Guid savingsAccountId,
            [Required] [FromBody] UpdateSavingsAccountRequest request)
        {
            _logger.LogInformation($"UpdateSavingsAccountForUser start. UserId: {userId}. SavingsAccountId: {savingsAccountId}");
            await _savingsAccountManager.UpdateSavingsAccountForUserAsync(userId, savingsAccountId, request);
            _logger.LogInformation($"UpdateSavingsAccountForUser end. UserId: {userId}. SavingsAccountId: {savingsAccountId}");
            return Ok();
        }
    }
}