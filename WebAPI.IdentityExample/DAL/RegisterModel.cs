using System.ComponentModel.DataAnnotations;

namespace WebAPI.IdentityExample.DAL;

public class RegisterModel
{
    [Required]
    [MinLength(4)]
    public required string Username { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MinLength(8)]
    public required string Password { get; set; }
}
