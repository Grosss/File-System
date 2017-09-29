using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using PL.Models;

namespace PL.Controllers
{
    public class HomeController : Controller
    {
		//[Authorize]
        public ActionResult Index(string path)
        {
			var realPath = Server.MapPath("~/Content/" + path);

			if (System.IO.File.Exists(realPath))
			{
				//http://stackoverflow.com/questions/1176022/unknown-file-type-mime
				return base.File(realPath, "application/octet-stream");
			}
			else
			{
				if (System.IO.Directory.Exists(realPath))
				{

					Uri url = Request.Url;
					//Every path needs to end with slash
					if (url.ToString().Last() != '/')
					{
						Response.Redirect("/Home/" + path + "/");
					}

					var fileListModel = new List<FileModel>();

					var dirListModel = new List<DirModel>();

					var dirList = Directory.EnumerateDirectories(realPath);
					foreach (string dir in dirList)
					{
						var dirInfo = new DirectoryInfo(dir);

						var dirModel = new DirModel
						{
							Name = dirInfo.Name,
							LastAccessTime = dirInfo.LastAccessTime
						};

						dirListModel.Add(dirModel);
					}

					var fileList = Directory.EnumerateFiles(realPath);
					foreach (var file in fileList)
					{
						var f = new FileInfo(file);

						var fileModel = new FileModel();

						if (f.Extension.ToLower() != "php" && f.Extension.ToLower() != "aspx"
						    && f.Extension.ToLower() != "asp")
						{
							fileModel.Name = f.Name;
							fileModel.LastAccessTime = f.LastAccessTime;
							fileModel.FileSizeText = (f.Length < 1024) ? f.Length.ToString() + " B" : f.Length / 1024 + " KB";

							fileListModel.Add(fileModel);
						}
					}

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
}
