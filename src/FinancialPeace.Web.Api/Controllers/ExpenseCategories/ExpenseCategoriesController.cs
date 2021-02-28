using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Managers;
using FinancialPeace.Web.Api.Models.Requests.ExpenseCategories;
using FinancialPeace.Web.Api.Models.Responses.ExpenseCategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ExpenseCategoriesController> _logger;

        /// <summary>
        /// Creates a new instance of the Expense Categories Controller class.
        /// </summary>
        /// <param name="expenseCategoriesManager">The expense categories manager.</param>
        /// <param name="logger">The logger.</param>
        public ExpenseCategoriesController(
            IExpenseCategoriesManager expenseCategoriesManager,
            ILogger<ExpenseCategoriesController> logger)
        {
            _expenseCategoriesManager = expenseCategoriesManager;
            _logger = logger;
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
            _logger.LogInformation("GetExpenseCategories start");
            var response = await _expenseCategoriesManager.GetExpenseCategoriesAsync();
            _logger.LogInformation("GetExpenseCategories end");
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
            _logger.LogInformation($"GetExpenseCategoriesForUser start. UserId: {userId}");
            var response = await _expenseCategoriesManager.GetExpenseCategoriesForUserAsync(userId);
            _logger.LogInformation($"GetExpenseCategoriesForUser end. UserId: {userId}");
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
            _logger.LogInformation($"AddExpenseCategoryForUser start. UserId: {userId}");
            await _expenseCategoriesManager.AddExpenseCategoryForUserAsync(userId, request);
            _logger.LogInformation($"AddExpenseCategoryForUser end. UserId: {userId}");
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
            _logger.LogInformation($"DeleteExpenseCategoryForUser start. UserId: {userId}. ExpenseCategoryId: {expenseCategoryId}");
            await _expenseCategoriesManager.DeleteExpenseCategoryForUserAsync(userId, expenseCategoryId);
            _logger.LogInformation($"DeleteExpenseCategoryForUser start. UserId: {userId}. ExpenseCategoryId: {expenseCategoryId}");
            return Ok();
        }
    }
}