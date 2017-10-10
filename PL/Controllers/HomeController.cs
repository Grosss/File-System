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
	    private const string RootDirectory = "~/Content/";

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
			var realPath = Server.MapPath(RootDirectory + path);
			
	        if (!Request.RawUrl.Contains(RouteData.Values["action"].ToString()))
	        {
				Response.Redirect("/Home/Index/");
	        }

	        if (Directory.Exists(realPath))
	        {
				if (Request.RawUrl.Last() != '/')
				{
					Response.Redirect("/Home/Index/" + path + "/");
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
			return Content(path + " is not a valid file or directory. " + RouteData.Values["controller"] + " " + RouteData.Values["action"]
				+ " " + RouteData.Values["path"]);
        }

		[HttpGet]
		public ActionResult CreateFolder()
		{
			return PartialView("_CreateFolder", new DirModel());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateFolder(DirModel directoryModel, string path = "")
		{
			var realPath = Server.MapPath(RootDirectory + path);

			if (Directory.Exists(realPath + directoryModel.Name))
			{
				ModelState.AddModelError("", "Such folder is already exists");
				return View("_CreateFolder", directoryModel);
			}

			if (ModelState.IsValid)
			{
				directoryService.CreateDirectory(realPath + directoryModel.Name);

				var dirListModel = directoryService.GetAllDirectories(realPath).Select(d => d.ToMvcDirectory());
				var fileListModel = fileService.GetAllFiles(realPath).Select(f => f.ToMvcFile());
				var explorerModel = new ExplorerModel
				{
					Directories = dirListModel,
					Files = fileListModel
				};
				return PartialView("_GetDirectories", explorerModel);
			}
			return PartialView(directoryModel);
		}

		[HttpGet]
		public ActionResult UploadFile()
		{
			return PartialView("_UploadFile");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult UploadFile(HttpPostedFileBase file, string path = "")
		{
			if (file.ContentLength > 0)
			{
				return RedirectToAction("Index", "Home");
			}
			if (ModelState.IsValid)
			{
				var fileName = Path.GetFileName(file.FileName);
				var folderPath = Server.MapPath(RootDirectory + path);
				var realPath = Path.Combine(folderPath, fileName);
				file.SaveAs(realPath);

				var dirListModel = directoryService.GetAllDirectories(folderPath).Select(d => d.ToMvcDirectory());
				var fileListModel = fileService.GetAllFiles(folderPath).Select(f => f.ToMvcFile());
				var explorerModel = new ExplorerModel
				{
					Directories = dirListModel,
					Files = fileListModel
				};
				return PartialView("_GetDirectories", explorerModel);
			}
			return PartialView();
		}

		[Authorize]
		[ChildActionOnly]
		public ActionResult GetDirectories(string path = "")
		{
			var realPath = Server.MapPath("~/Content/" + path);

			if (!Request.RawUrl.Contains(RouteData.Values["controller"].ToString()))
			{
				Response.Redirect("/Home/Index/");
			}

			if (Directory.Exists(realPath))
			{
				if (Request.RawUrl.Last() != '/')
				{
					Response.Redirect("/Home/Index/" + path + "/");
				}

				var dirListModel = directoryService.GetAllDirectories(realPath).Select(d => d.ToMvcDirectory());
				var fileListModel = fileService.GetAllFiles(realPath).Select(f => f.ToMvcFile());

				var explorerModel = new ExplorerModel
				{
					Directories = dirListModel,
					Files = fileListModel
				};

				return PartialView("_GetDirectories", explorerModel);
			}
			return Content(path + "ZZZ is not a valid file or directory. ZZZ " + RouteData.Values["controller"] + " " + RouteData.Values["action"]
				+ " " + RouteData.Values["path"]);
		}
    }
}
