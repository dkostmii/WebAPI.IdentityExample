using OneOf;
using OneOf.Types;
using WebAPI.IdentityExample.Contract;
using WebAPI.IdentityExample.DAL;

namespace WebAPI.IdentityExample.Services;

public interface IAuthService
{
    Task<OneOf<AuthLoginResponse, None>> Login(LoginModel model);
    Task<OneOf<RegisterSuccessResponse, RegisterFailedResponse>> Register(RegisterModel model, string role = UserRole.User);
    Task SeedRoles();
}
