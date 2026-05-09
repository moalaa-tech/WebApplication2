using System.Security.Claims;

namespace CRM.WebApp.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? GetUserId(this ClaimsPrincipal user)
        {
            return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string? GetClaim(this ClaimsPrincipal user, string claimType)
        {
            return user?.FindFirst(claimType)?.Value;
        }

    }
}
