using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Managers;
using FinancialPeace.Web.Api.Models.Requests.ExpenseCategories;
using FinancialPeace.Web.Api.Models.Responses.ExpenseCategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.WebApi.Core.Errors;

namespace FinancialPeace.Web.Api.Controllers.ExpenseCategories
{
    /// <summary>
    /// Exposes functionality to manage expenses categories.
    /// </summary>
    [Authorize]
    [Route("expenseCategories")]
    public class ExpenseCategoriesController : ControllerBase
    {
        private readonly IExpenseCategoriesManager _expenseCategoriesManager;

        /// <summary>
        /// Creates a new instance of the Expense Categories Controller class.
        /// </summary>
        /// <param name="expenseCategoriesManager">The expense categories manager.</param>
        public ExpenseCategoriesController(IExpenseCategoriesManager expenseCategoriesManager)
        {
            _expenseCategoriesManager = expenseCategoriesManager ?? throw new ArgumentNullException(nameof(expenseCategoriesManager));
        }
        
        /// <summary>
        /// Gets all listed expense categories.
        /// </summary>
        /// <response code="200">A collection of available expense categories.</response>
        /// <response code="401">The token provided does not provide access to this resource.</response>
        /// <response code="403">The request was forbidden from proceeding.</response>
        /// <response code="500">Something went wrong with processing the request, likely due to environmental issues.</response>
        [HttpGet]
        [ProducesResponseType(typeof(GetExpenseCategoriesResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetExpenseCategories()
        {
            var response = await _expenseCategoriesManager.GetExpenseCategories().ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Gets all expense categories that are mapped to the user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <response code="200">A collection of available expense categories.</response>
        /// <response code="401">The token provided does not provide access to this resource.</response>
        /// <response code="403">The request was forbidden from proceeding.</response>
        /// <response code="500">Something went wrong with processing the request, likely due to environmental issues.</response>
        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(GetExpenseCategoriesForUserResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetExpenseCategoriesForUser([Required] [FromRoute] Guid userId)
        {
            var response = await _expenseCategoriesManager.GetExpenseCategoriesForUser(userId).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Persists a new expense category to the database if it does not already exist and links that newly created expense category to the user. This newly created expense category will be available for all users.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="request">The expense category details.</param>
        /// <response code="202">The expense category was successfully created.</response>
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
        public async Task<IActionResult> AddExpenseCategoryForUser(
            [Required] [FromRoute] Guid userId,
            [Required] [FromBody] AddExpenseCategoryRequest request)
        {
            await _expenseCategoriesManager.AddExpenseCategoryForUser(userId, request).ConfigureAwait(false);
            return Accepted();
        }

        /// <summary>
        /// Deletes the mapping between the user and the expense category from the database.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="expenseCategoryId">The expense category's unique identifier.</param>
        /// <response code="200">The mapping was successfully deleted.</response>
        /// <response code="401">The token provided does not provide access to this resource.</response>
        /// <response code="403">The request was forbidden from proceeding.</response>
        /// <response code="500">Something went wrong with processing the request, likely due to environmental issues.</response>
        [HttpDelete("user/{userId}/expense/{expenseCategoryId}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteExpenseCategoryForUser(
            [Required] [FromRoute] Guid userId,
            [Required] [FromRoute] Guid expenseCategoryId)
        {
            await _expenseCategoriesManager.DeleteExpenseCategoryForUser(userId, expenseCategoryId).ConfigureAwait(false);
            return Ok();
        }
    }
}