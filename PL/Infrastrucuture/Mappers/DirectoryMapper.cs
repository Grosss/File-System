using BLL.Interface.Entities;
using PL.Models;

namespace PL.Infrastrucuture.Mappers
{
	public static class DirectoryMapper
	{
		public static DirModel ToMvcDirectory(this DirectoryEntity directory)
		{
			return new DirModel
			{
				Name = directory.Name,
				LastAccessTime = directory.LastAccessTime
			};
		}

		public static DirectoryEntity ToBllDirectory(this DirModel directory)
		{
			return new DirectoryEntity
			{
				Name = directory.Name,
				LastAccessTime = directory.LastAccessTime
			};
		}
	}
}