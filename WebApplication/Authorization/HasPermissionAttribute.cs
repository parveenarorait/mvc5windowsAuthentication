using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication.Authorization
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        readonly Permissions _permission;
        readonly bool _superAdminAccessAllowed;
        public HasPermissionAttribute(Permissions permission, bool superAdminAccessAllowed = true)
        {
            _permission = permission;
            _superAdminAccessAllowed = superAdminAccessAllowed;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.User != null)
            {
                var forbiddenRoute = new RouteData();
                forbiddenRoute.Values["Controller"] = "Error";
                forbiddenRoute.Values["Action"] = "Forbidden";
                if (!filterContext.HttpContext.User.UserHasThisPermission(_permission, _superAdminAccessAllowed))
                {
                    var errorViewResult = new ViewResult { ViewName = "~/Views/Error/Index.cshtml" };
                    errorViewResult.ViewBag.Title = "Forbidden Access";
                    errorViewResult.ViewBag.ErrorHeader = "Access forbidden: 403";
                    errorViewResult.ViewBag.ErrorMessage = "You are not authorized to access the page.";
                    filterContext.Result = errorViewResult;
                }
            }
        }
    }
}