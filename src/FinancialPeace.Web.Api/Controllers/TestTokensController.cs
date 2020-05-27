using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Initializes a new instance of the TestTokensController.
        /// </summary>
        /// <param name="jwtService">The JWT service.</param>
        public TestTokensController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }  
  
        /// <summary>
        /// Generates a random JWT token.
        /// </summary>
        /// <returns>A random JWT token.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetRandomToken()  
        {  
            var token = _jwtService.GenerateSecurityToken("fake@email.com", DateTime.Now);  
            return Ok(token);
        }
    }
}