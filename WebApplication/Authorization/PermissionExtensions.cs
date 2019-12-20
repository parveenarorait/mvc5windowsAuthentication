using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using WebApplication.Extensions;

namespace WebApplication.Authorization
{
    public static class PermissionExtensions
    {
        public static bool UserHasThisPermission(this IPrincipal user, Permissions permission, bool superAdminAccessAllowed = true)
        {
            var claimsPrincipal = user as ClaimsPrincipal;
            if (claimsPrincipal == null || claimsPrincipal.Claims.IsNullOrEmpty())
                return false;
            var permissionClaim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.Permission);
            if (permissionClaim != null && permissionClaim.Value != null)
                return permissionClaim.Value.Split('|').Any(x => x == ((int)permission).ToString() || (superAdminAccessAllowed && x == ((int)Permissions.AccessAll).ToString()));
            return false;
        }
    }
}