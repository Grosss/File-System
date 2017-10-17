using BLL.Interface.Entities;
using PL.Models;

namespace PL.Infrastrucuture.Mappers
{
	public static class DirectoryMapper
	{
		public static DirectoryViewModel ToMvcDirectory(this DirectoryEntity directory)
		{
			return new DirectoryViewModel
			{
				Name = directory.Name,
				LastAccessTime = directory.LastAccessTime
			};
		}

		public static DirectoryEntity ToBllDirectory(this DirectoryViewModel directory)
		{
			return new DirectoryEntity
			{
				Name = directory.Name,
				LastAccessTime = directory.LastAccessTime
			};
		}
	}
}