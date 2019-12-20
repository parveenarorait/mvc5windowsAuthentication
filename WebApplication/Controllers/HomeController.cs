using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Authorization;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        [HasPermission(Permissions.HomePageAccess)]
        public ActionResult Index()
        {
            return View();
        }

        [HasPermission(Permissions.AboutPageAccess)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HasPermission(Permissions.AdminPrivileges)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}