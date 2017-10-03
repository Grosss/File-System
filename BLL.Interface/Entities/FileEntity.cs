using System;

namespace BLL.Interface.Entities
{
	public class FileEntity
	{
		public string Name { get; set; }
		public string FileSizeText { get; set; }
		public string Extension { get; set; }
		public DateTime LastAccessTime { get; set; }
	}
}