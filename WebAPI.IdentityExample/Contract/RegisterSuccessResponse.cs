using Microsoft.AspNetCore.Identity;

namespace WebAPI.IdentityExample.Contract;

public class RegisterSuccessResponse : AuthResponse
{
    public UserResponseData User { get; init; }

    public RegisterSuccessResponse(IdentityUser user)
    {
        Status = "RegisterSuccess";
        Message = "Successfully registered user.";

        User = new()
        {
            Email = user.Email!,
            Username = user.UserName!,
            SecurityStamp = user.SecurityStamp!,
        };
    }
}
