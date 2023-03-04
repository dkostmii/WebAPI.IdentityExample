namespace WebAPI.IdentityExample.Contract;

public class NonExistingRoleResponse : RegisterFailedResponse
{
    public required string Role { get; init; }

    public NonExistingRoleResponse()
    {
        Status = "NonExistingRole";
        Message = "Role does not exist.";
    }
}
