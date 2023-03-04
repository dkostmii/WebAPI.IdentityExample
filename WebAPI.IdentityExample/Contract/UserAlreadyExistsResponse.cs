namespace WebAPI.IdentityExample.Contract;

public class UserAlreadyExistsResponse : RegisterFailedResponse
{
    public UserAlreadyExistsResponse()
    {
        Status = "AlreadyExists";
        Message = "User already exists.";
    }
}
