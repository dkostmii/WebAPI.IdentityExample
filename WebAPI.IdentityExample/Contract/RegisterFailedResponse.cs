namespace WebAPI.IdentityExample.Contract;

public class RegisterFailedResponse : AuthResponse
{
    public RegisterFailedResponse()
    {
        Status = "RegisterFailed";
        Message = "Failed to create user account.";
    }
}
