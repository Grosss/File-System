using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using BLL.Interface.Services;
using Newtonsoft.Json.Linq;
using PL.Infrastrucuture.Mappers;
using PL.Models;

namespace PL.Controllers
{
    public class HomeController : Controller
    {
	    private const string RootDirectory = "~/ExplorerFolder/";

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

		[Authorize]
		[ChildActionOnly]
		public ActionResult GetDirectories(string path = "")
		{
			var realPath = Server.MapPath(RootDirectory + path);

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

		[Authorize(Roles = "admin")]
		[HttpGet]
		public ActionResult CreateFolder()
		{
			return PartialView("_CreateFolder", new DirModel());
		}

		[Authorize(Roles = "admin")]
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

		[Authorize(Roles = "admin")]
		[HttpGet]
		public ActionResult UploadFile()
		{
			return PartialView("_UploadFile");
		}

		[Authorize(Roles = "admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult UploadFile(HttpPostedFileBase file, string uriPath = "")
		{
			if (file == null)
			{
				return RedirectToAction("Index", "Home");
			}

			if (ModelState.IsValid)
			{
				var folderPath = Server.MapPath(RootDirectory + uriPath);
				
				var realPath = Path.Combine(folderPath, Path.GetFileName(file.FileName));
				file.SaveAs(realPath);

				return RedirectToAction("Index", "Home", new { path = uriPath });
			}
			return PartialView();
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		public ActionResult DeleteFile(string jsonObject)
		{
			JToken token = JObject.Parse(jsonObject);
			var name = (string) token.SelectToken("name");
			var type = (string) token.SelectToken("type");
			var path = (string) token.SelectToken("path");
			var realPath = Server.MapPath(RootDirectory + path);

			if (type.ToLower().Contains("folder"))
			{
				directoryService.DeleteDirectory(realPath + name);
			}
			else
			{
				fileService.DeleteFile(realPath + name + "." + type.ToLower());
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
    }
}
