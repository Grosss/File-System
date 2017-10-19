using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PL
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{driveName}/{*path}",
				defaults: new { controller = "Home", action = "Explorer", driveName = UrlParameter.Optional, path = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "Another",
				url: "{controller}/{action}/{*path}",
				defaults: new { controller = "Home", action = "Explorer", path = UrlParameter.Optional }
			);
		}
	}
}