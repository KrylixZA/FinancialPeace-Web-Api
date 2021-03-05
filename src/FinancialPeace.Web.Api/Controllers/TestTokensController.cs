using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.WebApi.Core.Security;

namespace FinancialPeace.Web.Api.Controllers
{
    /// <summary>
    /// A test API to resolve a JWT token.
    /// </summary>
    [Route("tokens")]
    [ExcludeFromCodeCoverage]
    public class TestTokensController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly ILogger<TestTokensController> _logger;

        /// <summary>
        /// Initializes a new instance of the TestTokensController.
        /// </summary>
        /// <param name="jwtService">The JWT service.</param>
        /// <param name="logger">The logger.</param>
        public TestTokensController(IJwtService jwtService, ILogger<TestTokensController> logger)
        {
            _jwtService = jwtService;
            _logger = logger;
        }  
  
        /// <summary>
        /// Generates a random JWT token.
        /// </summary>
        /// <returns>A random JWT token.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetRandomTokenAsync()  
        {  
            _logger.LogInformation("GetRandomTokenAsync start");
            var token = _jwtService.GenerateSecurityToken("fake@email.com", DateTime.Now);  
            return Ok(token);
        }
    }
}