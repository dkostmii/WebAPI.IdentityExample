using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.IdentityExample.Services;

public class JWTService : IJWTService
{
    private readonly JWTServiceOptions _options;

    public JWTService(IOptions<JWTServiceOptions> options)
    {
        _options = options.Value;
    }

    private byte[] SecretBytes
    {
        get => Encoding.UTF8.GetBytes(_options.Secret);
    }

    public JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(SecretBytes);

        return new JwtSecurityToken(
                issuer: _options.ValidIssuer,
                audience: _options.ValidAudience,
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
    }
}
