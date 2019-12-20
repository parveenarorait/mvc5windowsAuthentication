using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            Response.Redirect("~/html/error-page.html");
        }


        public ActionResult IISNotFound()
        {
            Response.StatusCode = System.Convert.ToInt32(System.Net.HttpStatusCode.NotFound);
            return NotFound();
        }

        public ActionResult NotFound()
        {
            ViewBag.Title = "Page Not Found";
            ViewBag.ErrorHeader = "Page not found: 404";
            ViewBag.ErrorMessage = "Page you are looking is not found.";
            return View("Index");
        }

        public ActionResult Forbidden()
        {
            ViewBag.Title = "Forbidden Access";
            ViewBag.ErrorHeader = "Access forbidden: 403";
            ViewBag.ErrorMessage = "You are not authorized to access the page.";
            return View("Index");
        }

        public ActionResult InternalServerError()
        {
            ViewBag.Title = "Internal Server Error";
            ViewBag.ErrorHeader = "Internal server error occurred";
            ViewBag.ErrorMessage = "Some internal error has occurred, please try again after sometime.";
            return View("Index");
        }
    }
}