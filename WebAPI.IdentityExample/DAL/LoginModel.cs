using System.ComponentModel.DataAnnotations;

namespace WebAPI.IdentityExample.DAL;

public class LoginModel
{
    [Required]
    [MinLength(4)]
    public required string Username { get; set; }

    [Required]
    [MinLength(8)]
    public required string Password { get; set; }
}
