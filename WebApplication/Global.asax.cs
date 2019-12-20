using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            RouteData route = new RouteData();
            route.Values["Controller"] = "Error";
            if (exception is HttpException httpException)
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        route.Values["Action"] = "NotFound";
                        Response.StatusCode = 404;
                        break;
                    case 403:
                        route.Values["Action"] = "Forbidden";
                        Response.StatusCode = 403;
                        break;
                    default:
                        route.Values["Action"] = "InternalServerError";
                        Response.StatusCode = 500;
                        break;
                }
            }
            else
            {
                route.Values["Action"] = "InternalServerError";
                Response.StatusCode = 500;
            }
            Server.ClearError();
            Response.TrySkipIisCustomErrors = true;
            IController errorController = new Controllers.ErrorController();
            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), route));
        }

        protected void Application_PreSendRequestHeaders(object source, EventArgs e)
        {
            if (Response.Headers.AllKeys.Contains("Server")) Response.Headers.Remove("Server");
            if (Response.Headers.AllKeys.Contains("X-AspNet-Version")) Response.Headers.Remove("X-AspNet-Version");
            if (Response.Headers.AllKeys.Contains("X-AspNetMvc-Version")) Response.Headers.Remove("X-AspNetMvc-Version");
            if (Response.Headers.AllKeys.Contains("X-Powered-By")) Response.Headers.Remove("X-Powered-By");
            if (Response.Headers.AllKeys.Contains("X-SourceFiles")) Response.Headers.Remove("X-SourceFiles");
        }
    }
}
