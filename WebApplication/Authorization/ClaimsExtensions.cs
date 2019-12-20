using System.Security.Claims;
using System.Security.Principal;

namespace WebApplication.Authorization
{
    public static class ClaimsExtensions
    {
        public static string GivenName(this IPrincipal user)
        {
            return user.GetClaimValue(ClaimTypes.GivenName);
        }

        public static string Name(this IPrincipal user)
        {
            return user.GetClaimValue(ClaimTypes.Name);
        }

        public static string GetClaimValue(this IPrincipal user, string name)
        {
            var claimsIdentity = user.Identity as ClaimsIdentity;
            return claimsIdentity?.FindFirst(name)?.Value;
        }
    }
}