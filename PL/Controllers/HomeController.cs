using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;
using PL.Filters;
using PL.Infrastrucuture.Mappers;
using PL.Models;

namespace PL.Controllers
{
	[CustomErrorHandler]
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
	    [HttpPost]
	    public ActionResult FileSearch(string driveName = "", string path = "", string searchResult = "")
	    {
			var uri = new Uri(driveName + ":/" + path);
			var realPath = uri.LocalPath;

		    try
		    {
			    var files = fileService.GetFilesInSubdirectories(realPath, searchResult);
				
				return PartialView("_GetFiles", files.Select(f => f.ToMvcFile()));
		    }
		    catch (ArgumentException e)
		    {
			    var explorerModel = GetExplorerModel(realPath);
			    return PartialView("_GetDirectories", explorerModel);
		    }
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
				TotalFreeSpace = string.Format("{0} GB", (int) (drive.TotalFreeSpace / Math.Pow(2, 30))),
				TotalSize = string.Format("{0} GB", (int) (drive.TotalSize / Math.Pow(2, 30)))
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
			return RedirectToAction("NotFound", "Error");
		}
		
		[Authorize]
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
					Response.Redirect("/Home/Explorer/" + driveName + "/" + path + "/");
				}

				var explorerModel = GetExplorerModel(realPath);

				return PartialView("_GetDirectories", explorerModel);
			}
			return RedirectToAction("NotFound", "Error");
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
			return PartialView("_CreateFolder", directoryModel);
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
			return PartialView("_UploadFile");
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

	    #region private methods

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

	    #endregion
    }
}
