using System.Runtime.Serialization;

namespace WebAPI.IdentityExample.DAL;

public static class UserRole
{
    public const string Admin = "Admin";
    public const string User = "User";
    public static readonly string[] Roles = new[] { Admin, User };
}
