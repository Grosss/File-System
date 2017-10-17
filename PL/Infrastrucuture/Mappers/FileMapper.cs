using BLL.Interface.Entities;
using PL.Models;

namespace PL.Infrastrucuture.Mappers
{
	public static class FileMapper
	{
		public static FileViewModel ToMvcFile(this FileEntity file)
		{
			return new FileViewModel
			{
				Name = file.Name,
				FileSizeText = file.FileSizeText,
				Extension = file.Extension ?? "",
				LastAccessTime = file.LastAccessTime
			};
		}

		public static FileEntity ToBllFile(this FileViewModel fileView)
		{
			return new FileEntity
			{
				Name = fileView.Name,
				FileSizeText = fileView.FileSizeText,
				Extension = fileView.Extension,
				LastAccessTime = fileView.LastAccessTime
			};
		}
	}
}