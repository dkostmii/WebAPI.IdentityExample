namespace WebAPI.IdentityExample.Contract;

public abstract class AuthResponse
{
    public string Status { get; init; } = "";
    public string? Message { get; init; }
}
