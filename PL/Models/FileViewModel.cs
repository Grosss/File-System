using System;

namespace PL.Models
{
	public class FileViewModel
	{
		public string Name { get; set; }
		public string FileSizeText { get; set; }
		public string Extension { get; set; }
		public DateTime LastAccessTime { get; set; }
	}
}