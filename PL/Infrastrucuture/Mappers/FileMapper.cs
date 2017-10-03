using BLL.Interface.Entities;
using PL.Models;

namespace PL.Infrastrucuture.Mappers
{
	public static class FileMapper
	{
		public static FileModel ToMvcFile(this FileEntity file)
		{
			return new FileModel
			{
				Name = file.Name,
				FileSizeText = file.FileSizeText,
				Extension = file.Extension,
				LastAccessTime = file.LastAccessTime
			};
		}

		public static FileEntity ToBllFile(this FileModel file)
		{
			return new FileEntity
			{
				Name = file.Name,
				FileSizeText = file.FileSizeText,
				Extension = file.Extension,
				LastAccessTime = file.LastAccessTime
			};
		}
	}
}