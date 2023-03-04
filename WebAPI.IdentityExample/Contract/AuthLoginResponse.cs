namespace WebAPI.IdentityExample.Contract;

public class AuthLoginResponse : AuthResponse
{
    public required string Token { get; init; }
    public required DateTime ValidTo { get; init; }

    public AuthLoginResponse()
    {
        Status = "LoginSuccess";
        Message = "Successfully logged in.";
    }
}
