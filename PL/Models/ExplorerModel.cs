using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.Models
{
	public class ExplorerModel
	{
		public IEnumerable<DirectoryViewModel> Directories;
		public IEnumerable<FileViewModel> Files;
	}
}