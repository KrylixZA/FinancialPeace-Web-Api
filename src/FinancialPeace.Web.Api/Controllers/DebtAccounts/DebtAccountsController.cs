using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Managers;
using FinancialPeace.Web.Api.Models.Requests.DebtAccounts;
using FinancialPeace.Web.Api.Models.Responses.DebtAccounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Creates a new instance of the Debt Accounts Controller.
        /// </summary>
        /// <param name="debtAccountsManager">The debt accounts manager.</param>
        public DebtAccountsController(IDebtAccountsManager debtAccountsManager)
        {
            _debtAccountsManager = debtAccountsManager;
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
            var response = await _debtAccountsManager.GetDebtAccountForUser(userId);
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
            await _debtAccountsManager.AddDebtAccountForUser(userId, request);
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
            await _debtAccountsManager.AddAmountToDebtAccountForUser(userId, debtAccountId, request);
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
            await _debtAccountsManager.SubtractAmountFromDebtAccountForUser(userId, debtAccountId, request);
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
            await _debtAccountsManager.DeleteDebtAccountForUser(userId, debtAccountId);
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
            await _debtAccountsManager.UpdateDebtAccountForUser(userId, debtAccountId, request);
            return Ok();
        }
    }
}