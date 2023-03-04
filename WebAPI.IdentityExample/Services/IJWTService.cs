using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebAPI.IdentityExample.Services;

public interface IJWTService
{
    JwtSecurityToken GetToken(IEnumerable<Claim> claims);
}
