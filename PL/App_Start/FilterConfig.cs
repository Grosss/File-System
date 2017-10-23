using System.Web.Mvc;
using PL.Filters;

namespace PL
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			filters.Add(new CustomErrorHandler());
		}
	}
}