using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PL.Filters
{
	public class CustomErrorHandler : FilterAttribute, IExceptionFilter
	{
		public void OnException(ExceptionContext filterContext)
		{
			if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
				return;

			int statusCode = (int)HttpStatusCode.InternalServerError;
			HttpException exception = filterContext.Exception as HttpException;
			if (exception != null)
			{
				if (filterContext.Exception == null)
				{
					statusCode = default(HttpException).GetHttpCode();
				}
				else
				{
					HttpException casted = exception;
					statusCode = casted.GetHttpCode();
				}
			}
			else if (filterContext.Exception is UnauthorizedAccessException)
			{
				statusCode = (int)HttpStatusCode.Forbidden;
			}

			ActionResult result = CreateActionResult(filterContext, statusCode);
			filterContext.Result = result;

			filterContext.ExceptionHandled = true;
			filterContext.HttpContext.Response.Clear();
			filterContext.HttpContext.Response.StatusCode = statusCode;
			filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
		}

		protected virtual ActionResult CreateActionResult(ExceptionContext filterContext, int statusCode)
		{
			ControllerContext ctx = new ControllerContext(filterContext.RequestContext, filterContext.Controller);
			string statusCodeName = ((HttpStatusCode)statusCode).ToString();

			string viewName = SelectFirstView(ctx,
				string.Format("~/Views/Error/{0}.cshtml", statusCodeName),
				"~/Views/Shared/Error.cshtml",
				statusCodeName,
				"Error");

			string controllerName = (string)filterContext.RouteData.Values["controller"];
			string actionName = (string)filterContext.RouteData.Values["action"];
			HandleErrorInfo model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
			ViewResult result = new ViewResult
			{
				ViewName = viewName,
				ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
			};
			result.ViewBag.StatusCode = statusCode;
			return result;
		}

		protected string SelectFirstView(ControllerContext ctx, params string[] viewNames)
		{
			return viewNames.First(view => ViewExists(ctx, view));
		}

		protected bool ViewExists(ControllerContext ctx, string name)
		{
			ViewEngineResult result = ViewEngines.Engines.FindView(ctx, name, null);
			return result.View != null;
		}
	}
}