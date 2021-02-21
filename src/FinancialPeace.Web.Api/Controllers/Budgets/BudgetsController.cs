using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Managers;
using FinancialPeace.Web.Api.Models.Requests.Budgets;
using FinancialPeace.Web.Api.Models.Responses.Budgets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.WebApi.Core.Errors;

namespace FinancialPeace.Web.Api.Controllers.Budgets
{
    /// <summary>
    /// Exposes functionality to view and manage a user's budget.
    /// </summary>
    [Authorize]
    [Route("budgets")]
    public class BudgetsController : ControllerBase
    {
        private readonly IBudgetsManager _budgetsManager;

        /// <summary>
        /// Initializes a new instance of the Budgets Controller class.
        /// </summary>
        /// <param name="budgetsManager"></param>
        public BudgetsController(IBudgetsManager budgetsManager)
        {
            _budgetsManager = budgetsManager;
        }
        
        /// <summary>
        /// Gets the user's budget with all the listed expenses.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <response code="200">A collection of expenses linked to a user.</response>
        /// <response code="401">The token provided does not provide access to this resource.</response>
        /// <response code="403">The request was forbidden from proceeding.</response>
        /// <response code="500">Something went wrong with processing the request, likely due to environmental issues.</response>
        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(GetBudgetForUserResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetBudgetForUser([Required] [FromRoute] Guid userId)
        {
            var response = await _budgetsManager.GetBudgetForUserAsync(userId).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Attempts to create an expense for a user. If the user has no budget, it will create a budget for the user and then associate the expense with the budget.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="request">The expense details.</param>
        /// <response code="200">The expense was successfully created for the user.</response>
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
        public async Task<IActionResult> CreateExpenseForUser(
            [Required] [FromRoute] Guid userId,
            [Required] [FromBody] CreateExpenseRequest request)
        {
            await _budgetsManager.CreateExpenseForUserAsync(userId, request).ConfigureAwait(false);
            return Accepted();
        }

        /// <summary>
        /// Attempts to delete an expense for a user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="expenseId">The expense unique identifier.</param>
        /// <response code="200">The expense was successfully deleted for the user.</response>
        /// <response code="401">The token provided does not provide access to this resource.</response>
        /// <response code="403">The request was forbidden from proceeding.</response>
        /// <response code="500">Something went wrong with processing the request, likely due to environmental issues.</response>
        [HttpDelete("user/{userId}/expense/{expenseId}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteExpenseForUser(
            [Required] [FromRoute] Guid userId,
            [Required] [FromRoute] Guid expenseId)
        {
            await _budgetsManager.DeleteExpenseForUserAsync(userId, expenseId).ConfigureAwait(false);
            return Ok();
        }

        /// <summary>
        /// Attempts to update the details of an expense for a user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="expenseId">The expense unique identifier.</param>
        /// <param name="request">The updated expense details.</param>
        /// <response code="200">The expense was successfully deleted for the user.</response>
        /// <response code="400">The request was badly formed.</response>
        /// <response code="401">The token provided does not provide access to this resource.</response>
        /// <response code="403">The request was forbidden from proceeding.</response>
        /// <response code="500">Something went wrong with processing the request, likely due to environmental issues.</response>
        [HttpPatch("user/{userId}/expense/{expenseId}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateExpenseForUser(
            [Required] [FromRoute] Guid userId,
            [Required] [FromRoute] Guid expenseId,
            [Required] [FromBody] UpdateExpenseRequest request)
        {
            await _budgetsManager.UpdateExpenseForUserAsync(userId, expenseId, request).ConfigureAwait(false);
            return Ok();
        }
    }
}