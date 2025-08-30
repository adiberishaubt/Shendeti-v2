using System.Security.Claims;

namespace Shendeti.Extensions;

public static class UserExtensions
{
    public static string GetId(this ClaimsPrincipal principal)
    {
        return principal.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
    }
    
    public static string GetRole(this ClaimsPrincipal principal)
    {
        return principal.Claims.First(c => c.Type == ClaimTypes.Role).Value;
    }
}