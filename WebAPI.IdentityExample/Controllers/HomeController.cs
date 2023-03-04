using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.IdentityExample.Controllers
{
    [AllowAnonymous]
    [Route("api")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var host = (Request.IsHttps ? "https://" : "http://") + Request.Host;

            return Ok(new Dictionary<string, string>
            {
                { "login", $"{host}/api/auth/login" },
                { "register", $"{host}/api/auth/register" },
                { "weatherForecast", $"{host}/api/weatherForecast" },
            });
        }
    }
}
