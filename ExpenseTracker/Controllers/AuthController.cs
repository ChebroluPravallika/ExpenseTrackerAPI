using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authenticateService;
        public AuthController(IAuthService authenticateService)
        {
            this._authenticateService = authenticateService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserDTO model)
        {
            return Ok(await _authenticateService.Login(model));
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO model)
        {
            return Ok(await _authenticateService.Register(model));
        }
    }
}
