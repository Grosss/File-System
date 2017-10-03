using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using BLL.Interface.Services;
using PL.Infrastrucuture.Mappers;
using PL.Models;

namespace PL.Controllers
{
    public class HomeController : Controller
    {
		private readonly IFileService fileService;
		private readonly IDirectoryService directoryService;

		public HomeController(IFileService fileService, IDirectoryService directoryService)
		{
			this.fileService = fileService;
			this.directoryService = directoryService;
		}
		
		[Authorize]
		[HttpGet]
        public ActionResult Index(string path = "")
        {
			var realPath = Server.MapPath("~/Content/" + path);

			if (System.IO.File.Exists(realPath))
			{
				return File(realPath, "application/octet-stream");
			}
	        
	        if (!Request.RawUrl.Contains(ControllerContext.RequestContext.RouteData.Values["controller"].ToString()))
	        {
				Response.Redirect("/Home/");
	        }

	        if (Directory.Exists(realPath))
	        {
				if (Request.RawUrl.Last() != '/')
		        {
			        Response.Redirect("/Home/" + path + "/");
		        }

				var dirListModel = directoryService.GetAllDirectories(realPath).Select(d => d.ToMvcDirectory());
		        var fileListModel = fileService.GetAllFiles(realPath).Select(f => f.ToMvcFile());
				
		        var explorerModel = new ExplorerModel
		        {
					Directories = dirListModel,
			        Files = fileListModel
		        };

		        return View(explorerModel);
	        }
	        return Content(path + " is not a valid file or directory.");
        }

    }
}
