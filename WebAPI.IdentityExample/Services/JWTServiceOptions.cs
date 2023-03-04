using System.ComponentModel.DataAnnotations;

namespace WebAPI.IdentityExample.Services;

public class JWTServiceOptions
{
    public const string JWTService = "JWTService";

    [Required]
    [Url]
    public required string ValidAudience { get; set; }

    [Required]
    [Url]
    public required string ValidIssuer { get; set; }

    [Required]
    [MinLength(16)]
    public required string Secret { get; set; }
}
