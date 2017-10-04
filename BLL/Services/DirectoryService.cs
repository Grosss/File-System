using System.Collections.Generic;
using System.IO;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;

namespace BLL.Services
{
	public class DirectoryService : IDirectoryService
	{
		public IEnumerable<DirectoryEntity> GetAllDirectories(string path)
		{
			return Directory.EnumerateDirectories(path)
				.Select(directoryName => new DirectoryInfo(directoryName))
				.Select(directoryInfo => new DirectoryEntity
				{
					Name = directoryInfo.Name, LastAccessTime = directoryInfo.LastAccessTime
				})
				.ToList();
		}

		public DirectoryEntity GetDirectory(int id)
		{
			throw new System.NotImplementedException();
		}

		public void CreateDirectory(DirectoryEntity entity)
		{
			throw new System.NotImplementedException();
		}

		public void DeleteDirectory(string path)
		{
			if (Directory.Exists(path))
			{
				Directory.Delete(path, true);
			}
		}
	}
}