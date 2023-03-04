using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.IdentityExample.DAL;
using WebAPI.IdentityExample.Services;

namespace WebAPI.IdentityExample.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var loginResult = await _service.Login(model);

            return loginResult.Match<IActionResult>(
                    success => Ok(success),
                    _ => Unauthorized()
                );
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromQuery] string? userRole, [FromBody] RegisterModel model)
        {
            var registerResult = string.IsNullOrEmpty(userRole) ?
                await _service.Register(model) : await _service.Register(model, userRole.Capitalize());

            return registerResult.Match<IActionResult>(
                    success => Ok(success),
                    failed => BadRequest(failed)
                );
        }
    }
}
