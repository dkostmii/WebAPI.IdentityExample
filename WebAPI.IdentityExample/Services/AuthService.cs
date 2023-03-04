using Microsoft.AspNetCore.Identity;
using OneOf;
using OneOf.Types;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebAPI.IdentityExample.Contract;
using WebAPI.IdentityExample.DAL;

namespace WebAPI.IdentityExample.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJWTService _jwtService;

    public AuthService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IJWTService jwtService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtService = jwtService;
    }

    public async Task<OneOf<AuthLoginResponse, None>> Login(LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);

        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            return new None();

        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        authClaims.AddRange(
            userRoles.Select(role => new Claim(ClaimTypes.Role, role)).ToList());

        var token = _jwtService.GetToken(authClaims);

        return new AuthLoginResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ValidTo = token.ValidTo,
        };
    }

    public async Task<OneOf<RegisterSuccessResponse, RegisterFailedResponse>> Register(RegisterModel model, string role = UserRole.User)
    {
        var userExists = await _userManager.FindByNameAsync(model.Username);
        if (userExists is not null)
            return new UserAlreadyExistsResponse();

        var user = new IdentityUser
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };

        if (!await _roleManager.RoleExistsAsync(role))
            return new NonExistingRoleResponse { Role = role };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return new RegisterFailedResponse { Message = "Unable to create user." };

        result = await _userManager.AddToRoleAsync(user, role);

        if (!result.Succeeded)
            return new RegisterFailedResponse { Message = "Unable to add user to role." } ;

        return new RegisterSuccessResponse(user);
    }

    public async Task SeedRoles()
    {
        foreach (var role in UserRole.Roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
