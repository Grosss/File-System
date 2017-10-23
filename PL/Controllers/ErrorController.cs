using System.Web.Mvc;

namespace PL.Controllers
{
    public class ErrorController : Controller
	{
		public ViewResult NotFound()
		{
			Response.StatusCode = 404;
			return View("NotFound");
		}

		public ViewResult Forbidden()
		{
			Response.StatusCode = 403;
			return View("Forbidden");
		}

		public ViewResult BadRequest()
		{
			Response.StatusCode = 400;
			return View("BadRequest");
		}
    }
}