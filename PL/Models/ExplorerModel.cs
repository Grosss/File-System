using System.Collections.Generic;

namespace PL.Models
{
	public class ExplorerModel
	{
		public IEnumerable<DirectoryViewModel> Directories;
		public IEnumerable<FileViewModel> Files;
	}
}