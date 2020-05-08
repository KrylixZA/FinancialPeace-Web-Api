using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using FinancialPeace.Web.Api.Models.Requests.Currencies;
using FinancialPeace.Web.Api.Models.Responses.Currencies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.WebApi.Core.Errors;

namespace FinancialPeace.Web.Api.Controllers.Currencies
{
    /// <summary>
    /// Exposes functionality to manage currencies.
    /// </summary>
    [Authorize]
    [Route("currencies")]
    public class CurrenciesController : ControllerBase
    {
        /// <summary>
        /// Gets all the registered currencies from the database.
        /// </summary>
        /// <response code="200">An enumeration of registered currencies.</response>
        /// <response code="401">The token provided does not provide access to this resource.</response>
        /// <response code="403">The request was forbidden from proceeding.</response>
        /// <response code="500">Something went wrong with processing the request, likely due to environmental issues.</response>
        [HttpGet]
        [ProducesResponseType(typeof(GetCurrencyResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetCurrencies()
        {
            return Ok();
        }

        /// <summary>
        /// Persists a new currency to the database. If a currency already exists, it will still return 202 - Accepted.
        /// </summary>
        /// <param name="request">A request to create a new currency.</param>
        /// <response code="202">The currency was successfully created.</response>
        /// <response code="400">The request was badly formed.</response>
        /// <response code="401">The token provided does not provide access to this resource.</response>
        /// <response code="403">The request was forbidden from proceeding.</response>
        /// <response code="500">Something went wrong with processing the request, likely due to environmental issues.</response>
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddCurrency([Required] [FromBody] AddCurrencyRequest request)
        {
            return Accepted();
        }
    }
}