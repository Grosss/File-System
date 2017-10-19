using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
	    public ActionResult Index()
	    {
		    ViewBag.HostName = Environment.MachineName;
		    var drives = DriveInfo.GetDrives().Where(drive => drive.IsReady).Select(drive => new DriveViewModel
		    {
			    Name = drive.Name,
				VolumeLabel = drive.VolumeLabel,
				TotalFreeSpace = String.Format("{0} GB", (int) (drive.TotalFreeSpace / Math.Pow(2, 30))),
				TotalSize = String.Format("{0} GB", (int) (drive.TotalSize / Math.Pow(2, 30)))
		    });
		    return View("Index", drives);
	    }

		[Authorize]
		[HttpGet]
		public ActionResult Explorer(string driveName = "", string path = "")
		{
			if (string.IsNullOrEmpty(driveName))
			{
				return RedirectToAction("Index", "Home");
			}

			var uri = new Uri(driveName + ":/" + path);
			var realPath = uri.LocalPath;

			if (!Request.RawUrl.Contains(RouteData.Values["action"].ToString()))
			{
				Response.Redirect("/Home/Explorer/");
			}

			if (Directory.Exists(realPath))
			{
				if (Request.RawUrl.Last() != '/')
				{
					Response.Redirect("/Home/Explorer/" + path + "/");
				}

				var explorerModel = GetExplorerModel(realPath);

				return View(explorerModel);
			}
			return Content(path + " is not a valid file or directory. " + RouteData.Values["controller"] + " " + RouteData.Values["action"]
				+ " " + RouteData.Values["driveName"] + " " + RouteData.Values["path"]);
		}
		
		[Authorize]
		[ChildActionOnly]
		public ActionResult GetDirectories(string driveName = "", string path = "")
		{
			if (string.IsNullOrEmpty(driveName))
			{
				return RedirectToAction("Index", "Home");
			}

			var uri = new Uri(driveName + ":/" + path);
			var realPath = uri.LocalPath;

			if (!Request.RawUrl.Contains(RouteData.Values["controller"].ToString()))
			{
				Response.Redirect("/Home/Explorer/");
			}

			if (Directory.Exists(realPath))
			{
				if (Request.RawUrl.Last() != '/')
				{
					Response.Redirect("/Home/Explorer/" + path + "/");
				}

				var explorerModel = GetExplorerModel(realPath);

				return PartialView("_GetDirectories", explorerModel);
			}
			return Content(path + "ZZZ is not a valid file or directory. ZZZ " + RouteData.Values["controller"] + " " + RouteData.Values["action"]
				+ " " + RouteData.Values["driveName"] + " " + RouteData.Values["path"]);
		}
		
	    [Authorize(Roles = "admin")]
		[HttpGet]
		public ActionResult CreateFolder()
		{
			return PartialView("_CreateFolder", new DirectoryViewModel());
		}

		[Authorize(Roles = "admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateFolder(DirectoryViewModel directoryModel, string driveName = "", string path = "")
		{
			var uri = new Uri(driveName + ":/" + path);
			var realPath = uri.LocalPath;

			if (Directory.Exists(realPath + directoryModel.Name))
			{
				ModelState.AddModelError("", "Such folder is already exists");
				return View("_CreateFolder", directoryModel);
			}

			if (ModelState.IsValid)
			{
				directoryService.CreateDirectory(realPath + directoryModel.Name);

				var explorerModel = GetExplorerModel(realPath);
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
		public ActionResult UploadFile(HttpPostedFileBase file, string uriDrive = "", string uriPath = "")
		{
			if (file == null)
			{
				return RedirectToAction("Explorer", "Home");
			}

			if (ModelState.IsValid)
			{
				var uri = new Uri(uriDrive + ":/" + uriPath);
				var folderPath = uri.LocalPath;
				
				var realPath = Path.Combine(folderPath, Path.GetFileName(file.FileName));
				file.SaveAs(realPath);

				return RedirectToAction("Explorer", "Home", new { driveName = uriDrive, path = uriPath });
			}
			return PartialView();
		}
		
		[HttpPost]
		[Authorize(Roles = "admin")]
		public ActionResult DeleteFile(DeleteViewModel jsonObject)
		{
			var name = jsonObject.Name;
			var type = jsonObject.Type;
			var driveName = jsonObject.DriveName ?? "";
			var path = jsonObject.Path ?? "";

			var uri = new Uri(driveName + ":/" + path);
			var realPath = uri.LocalPath;

			if (type.ToLower().Contains("folder"))
			{
				directoryService.DeleteDirectory(realPath + name);
			}
			else
			{
				fileService.DeleteFile(realPath + name);
			}

			var explorerModel = GetExplorerModel(realPath);

			return PartialView("_GetDirectories", explorerModel);
		}

		private ExplorerModel GetExplorerModel(string realPath)
		{
			var dirListModel = directoryService.GetAllDirectories(realPath).Select(d => d.ToMvcDirectory());
			var fileListModel = fileService.GetAllFiles(realPath).Select(f => f.ToMvcFile());

			var explorerModel = new ExplorerModel
			{
				Directories = dirListModel,
				Files = fileListModel
			};
			return explorerModel;
		}
    }
}
