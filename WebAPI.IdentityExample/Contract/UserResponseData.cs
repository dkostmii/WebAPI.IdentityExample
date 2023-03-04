namespace WebAPI.IdentityExample.Contract;

public class UserResponseData
{
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string SecurityStamp { get; set; }
}
