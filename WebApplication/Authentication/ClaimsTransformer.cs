using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication.Authentication
{
    public class ClaimsTransformer : ClaimsAuthenticationManager
    {
        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            if (!incomingPrincipal.Identity.IsAuthenticated)
            {
                return base.Authenticate(resourceName, incomingPrincipal);
            }
            string userName = incomingPrincipal.Identity.Name.Split('\\')[1];
            var newPrincipal = CreateApplicationPrincipal(userName);
            EstablishSession(newPrincipal);
            return newPrincipal;
        }

        private void EstablishSession(ClaimsPrincipal newPrincipal)
        {
            var sessionSecurityToken = new SessionSecurityToken(newPrincipal, TimeSpan.FromMinutes(30));
            FederatedAuthentication.SessionAuthenticationModule.WriteSessionTokenToCookie(sessionSecurityToken);
        }

        private ClaimsPrincipal CreateApplicationPrincipal(string userName)
        {
            #region Commented code to get User from AD and Claims from IIdentityService Service
            //var logger = DependencyResolver.Current.GetService<ILogger>();
            //UserPrincipal userPrincipal = null;
            //using (var context = new PrincipalContext(ContextType.Domain, "subdomain.domain.net"))
            //{
            //    if (context != null)
            //        userPrincipal = UserPrincipal.FindByIdentity(context, userName);
            //}
            //if (userPrincipal == null)
            //{
            //    logger.LogWarning("UserPrincipal is null for {UserName}", userName);
            //}
            //Dictionary<string, string> userClaims = null;
            //var identityService = DependencyResolver.Current.GetService<IIdentityService>();
            //var task = Task.Run(async () => { userClaims = await identityService.GetUserClaims(userPrincipal.SamAccountName); });
            //task.Wait();
            var userClaims = new Dictionary<string, string>
            {
                {CustomClaimTypes.Permission, "1|3" },
                {CustomClaimTypes.Projects, "abc|xyz" },
                {CustomClaimTypes.UserId, "10" }
            };
            #endregion
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userName));
            //claims.Add(new Claim(CustomClaimTypes.DisplayName, userPrincipal.DisplayName));
            //claims.Add(new Claim(CustomClaimTypes.FirstName, userPrincipal.GivenName));
            claims.Add(new Claim(CustomClaimTypes.DisplayName, "DisplayName"));
            claims.Add(new Claim(CustomClaimTypes.FirstName, "GivenName"));
            if (userClaims != null)
            {
                foreach (var kvp in userClaims)
                {
                    claims.Add(new Claim(kvp.Key, kvp.Value));
                }
            }
            return new ClaimsPrincipal(new ClaimsIdentity(claims, "CustomPrincipal"));
        }
    }
}